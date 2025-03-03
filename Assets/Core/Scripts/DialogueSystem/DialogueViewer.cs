using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class DialogueViewer : MonoBehaviour
{
    [Header("Dev settings")]
    [SerializeField] DialogueBunch _dialogueBunch;
    [SerializeField] private bool _isActiveOnStart;
    [Space(30), Header("Maintenance settings")]
    [SerializeField] private Canvas _dialogueCanvas;
    [SerializeField] private Animator _dialogueAnimator;
    [SerializeField] private TMP_Text _simplePhraseChamber;
    [SerializeField] private TMP_Text _nameChamber;
    [SerializeField] private TMP_Text _gradeChamber;
    [SerializeField] private ButtonContainer _answersChamberLayoutGroup;
    [SerializeField, Tooltip("must contain MenuButton script")] private GameObject _answerButtonPrefab;
    private bool _isWriting = false;
    private Transform _answersChamberTransform;    
    private Coroutine _writingCoroutine;
    
    public static bool IsGoing { get; private set; } = false;
    public DialogueBaseClass CurrentDialogueElement { get; private set; }

    private void Start()
    {
        if (_answerButtonPrefab == null || _answerButtonPrefab.GetComponent<MenuButton>() == null)
        {
            Debug.LogError("GameObject dont have ButtonContainer or GameObjet dont initialised");
        }

        _answersChamberTransform = _answersChamberLayoutGroup.gameObject.transform;

        if (_isActiveOnStart)
        {
            StartCoroutine(Starter());
        }
        else
        {
            _dialogueCanvas.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (IsGoing && CurrentDialogueElement.TypeOfDialogue == TypeOfDialogue.SimplePhrases && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) && _simplePhraseChamber.text.Length > 1)
        {
            if (_isWriting)
            {
                _simplePhraseChamber.text = CurrentDialogueElement.simplePhrase.InputText;
                _isWriting = false;
                StopCoroutine(_writingCoroutine);
            }
            else
            {
                CurrentDialogueElement = SetNewElementAtSimplePhrase(_dialogueBunch.RootDialogue);
                ViewDialogue();
            }

        }
    }

    public IEnumerator Starter()
    {
        _dialogueCanvas.gameObject.SetActive(true);
        Reseter();
        _gradeChamber.text = string.Empty;
        CurrentDialogueElement = _dialogueBunch.RootDialogue[0];
        if (!IsGoing)
        {
            yield return new WaitForSeconds(GetCurrentAnim(_dialogueAnimator).length);
            IsGoing = true;
            ViewDialogue();
            _gradeChamber.text = "...";
        }
    }

    private IEnumerator Ender()
    {
        Reseter();
        _gradeChamber.text = string.Empty;
        IsGoing = false;
        _dialogueAnimator.SetTrigger("IsEnding");
        yield return new WaitForSeconds(GetCurrentAnim(_dialogueAnimator).length);
        _dialogueCanvas.gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void ViewDialogue()
    {
        StopAllCoroutines();
        Reseter();

        if (CurrentDialogueElement != null)
        {
            if (CurrentDialogueElement.TypeOfDialogue == TypeOfDialogue.SimplePhrases)
            {
                _nameChamber.text = CurrentDialogueElement.simplePhrase.InputName;
                _writingCoroutine = StartCoroutine(Writer.WritingText(CurrentDialogueElement.simplePhrase.InputText, _simplePhraseChamber, CurrentDialogueElement.SymbolTime, WritingTextComplition));
                _isWriting = true;
            }
            if (CurrentDialogueElement.TypeOfDialogue == TypeOfDialogue.Answers)
            {
                for (int i = 0; i < CurrentDialogueElement.Answers.Count; i++)
                {
                    MenuButton currentAnswerButton = Instantiate(_answerButtonPrefab, _answersChamberTransform).GetComponent<MenuButton>();
                    DialogueBaseClass nextDialogueElement = CurrentDialogueElement.Answers[i].NextDialogueBaseClasses[0];
                    float addReputation = CurrentDialogueElement.Answers[i].AddReputation;
                    currentAnswerButton.OnPressMethod.AddListener(() => SetNewElementAtAnswer(nextDialogueElement, addReputation));

                    //StartCoroutine(DialogueBaseClass.WritingText(CurrentDialogueElement.Answers[i].InputText, currentAnswerButton.TextChamber, CurrentDialogueElement.SymbolTime));
                    currentAnswerButton.TextChamber.text = CurrentDialogueElement.Answers[i].InputText;
                    _answersChamberLayoutGroup.AddButton(currentAnswerButton.TextChamber);
                }
            }
        }
        else
        {
            StartCoroutine(Ender());
        }
    }

    //надо переписать, после вопроса если идет вопрос (те последней до перехода к новому вопросу была простая фраза), закрывается книжка
    private DialogueBaseClass SetNewElementAtSimplePhrase(List<DialogueBaseClass> dialogue)
    {
        DialogueBaseClass currentDialogueElement = null;
        if (CurrentDialogueElement.TypeOfDialogue == TypeOfDialogue.SimplePhrases)
        {
            if (dialogue.Contains(CurrentDialogueElement))
            {
                if (dialogue[dialogue.Count - 1] == CurrentDialogueElement)
                {
                    //когда конечный в данном листе
                    Debug.Log("-1");
                    return null;
                    
                }
                currentDialogueElement = dialogue[dialogue.IndexOf(CurrentDialogueElement) + 1];
                return currentDialogueElement;
            }
            //если нам надо спуститься глубже и в данном листе нет того чего мы ищем
            for (int i = 0; i < dialogue.Count; i++)
            {
                if (dialogue[i].TypeOfDialogue == TypeOfDialogue.Answers)
                {
                    foreach (DialogueBaseClass.Answer answer in dialogue[i].Answers)
                    {
                        //??
                        currentDialogueElement = SetNewElementAtSimplePhrase(answer.NextDialogueBaseClasses);
                        if (currentDialogueElement != null)
                        {
                            return currentDialogueElement;
                        }
                    }
                }
            }
            //for (int i = 0; i < dialogue.Count - 1; i++)
            //{
            //    if (dialogue[i].TypeOfDialogue == TypeOfDialogue.Answers)
            //    {
            //        foreach (DialogueBaseClass.Answer answer in dialogue[i].Answers)
            //        {
            //            if (answer.NextDialogueBaseClasses.Contains(CurrentDialogueElement))
            //            {
            //                currentDialogueElement = dialogue[i + 1];
            //                return currentDialogueElement;
            //            }
            //        }
            //    }
            //}
        }
        return null;
    }

    public void SetNewElementAtAnswer(DialogueBaseClass currentDialogueElement, float addReputation)
    {
        if(CurrentDialogueElement.TypeOfDialogue == TypeOfDialogue.Answers)
        {
            _dialogueBunch.Reputation += addReputation;
            CurrentDialogueElement = currentDialogueElement;
            
            ViewDialogue();
            SetGrade(_dialogueBunch.Reputation);
        }
    }

    private void Reseter()
    {
        _simplePhraseChamber.color = Color.black;
        _nameChamber.color = Color.black;
        _gradeChamber.color = Color.red;
        _simplePhraseChamber.text = string.Empty;
        _nameChamber.text = string.Empty;
        foreach (Transform child in _answersChamberTransform)
        {
            _answersChamberLayoutGroup.Buttons.Clear();
            Destroy(child.gameObject);
        }
    }


    //for next evolution
    private AnimationClip GetCurrentAnim(Animator anim)
    {
        foreach(AnimationClip clip in anim.runtimeAnimatorController.animationClips)
        {
            return clip;
        }
        return null;
    }
    
    private void WritingTextComplition()
    {
        _isWriting = false;
    }

    private void SetGrade(float reputation)
    {
        if (reputation > _dialogueBunch.MaxReputation)
        {
            _gradeChamber.text = "5:)";
            return;
        }
        if(reputation < _dialogueBunch.MinReputation)
        {
            _gradeChamber.text = "2;(";
            return;
        }
        if(reputation > (_dialogueBunch.MinReputation+_dialogueBunch.MaxReputation)/2)
        {
            _gradeChamber.text = "4";
            return;
        }
        if (reputation > (_dialogueBunch.MinReputation - _dialogueBunch.MaxReputation) / 2)
        {
            _gradeChamber.text = "3..";
            return;
        }
    }
}
