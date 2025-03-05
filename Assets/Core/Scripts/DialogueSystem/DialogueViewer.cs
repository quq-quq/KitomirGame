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
    [Space(40), Header("Maintenance settings")]
    [SerializeField] private Canvas _dialogueCanvas;
    [Space(10)]
    [SerializeField] private Animator _dialogueAnimator;
    [SerializeField] private string _triggerForEndName;
    [Space(10)]
    [SerializeField] private TMP_Text _simplePhraseChamber;
    [SerializeField] private TMP_Text _nameChamber;
    [SerializeField] private TMP_Text _gradeChamber;
    [SerializeField] private ButtonContainer _answersChamberLayoutGroup;
    [Space(10)]
    [SerializeField, Tooltip("must contain MenuButton script")] private GameObject _answerButtonPrefab;
    private bool _isWriting = false;
    private Transform _answersChamberTransform;    
    private Coroutine _writingCoroutine;
    private DialogueSeter _dialogueSeter;
    //private List<DialogueBaseClass> _previousAnswers;

    public static bool IsGoing { get; private set; } = false;
    public DialogueBaseClass CurrentDialogueElement { get; private set; }

    private void Start()
    {
        if (_answerButtonPrefab == null || _answerButtonPrefab.GetComponent<MenuButton>() == null)
        {
            Debug.LogError("GameObject dont have ButtonContainer or GameObjet dont initialised");
        }

        _answersChamberTransform = _answersChamberLayoutGroup.gameObject.transform;
        _dialogueSeter = new DialogueSeter(_dialogueBunch);

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
                CurrentDialogueElement = _dialogueSeter.SetNewElementAtSimplePhrase(_dialogueBunch.RootDialogue, CurrentDialogueElement);
                ViewDialogue();
            }

        }
    }

    public IEnumerator Starter()
    {
        _dialogueCanvas.gameObject.SetActive(true);
        Reseter();
        CurrentDialogueElement = _dialogueBunch.RootDialogue[0];
        if (!IsGoing)
        {
            yield return new WaitForSeconds(_dialogueAnimator.GetCurrentAnimatorStateInfo(0).length);
            IsGoing = true;
            ViewDialogue();
            _gradeChamber.text = "...";
        }
    }

    private IEnumerator Ender()
    {
        Reseter();        
        IsGoing = false;
        _dialogueAnimator.SetTrigger(_triggerForEndName);
        yield return new WaitForSeconds(_dialogueAnimator.GetCurrentAnimatorStateInfo(0).length);
        _dialogueCanvas.gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void ViewDialogue()
    {
        StopAllCoroutines();
        Reseter();
        ViewGrade(_dialogueBunch.Reputation, _gradeChamber);

        if (CurrentDialogueElement != null)
        {
            if (CurrentDialogueElement.TypeOfDialogue == TypeOfDialogue.SimplePhrases)
            {
                _nameChamber.text = CurrentDialogueElement.simplePhrase.InputName;
                _writingCoroutine = StartCoroutine(DialogueWriter.SimpleWritingText(CurrentDialogueElement.simplePhrase.InputText, _simplePhraseChamber, CurrentDialogueElement.SymbolTime, WritingTextComplition));
                _isWriting = true;
            }
            if (CurrentDialogueElement.TypeOfDialogue == TypeOfDialogue.Answers)
            {
                for (int i = 0; i < CurrentDialogueElement.Answers.Count; i++)
                {
                    MenuButton currentAnswerButton = Instantiate(_answerButtonPrefab, _answersChamberTransform).GetComponent<MenuButton>();
                    DialogueBaseClass nextDialogueElement = CurrentDialogueElement.Answers[i].NextDialogueBaseClasses[0];
                    float addReputation = CurrentDialogueElement.Answers[i].AddReputation;
                    currentAnswerButton.OnPressMethod.AddListener(() => SetNewElementAtAnswerBufer(nextDialogueElement, addReputation));
                    currentAnswerButton.OnPressMethod.AddListener(ViewDialogue);

                    //StartCoroutine(DialogueBaseClass.SimpleWritingText(_currentDialogueElement.Answers[i].InputText, currentAnswerButton.TextChamber, _currentDialogueElement.SymbolTime));
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

    private void ViewGrade(float reputation, TMP_Text gradeChamber)
    {
        if (reputation > _dialogueBunch.MaxReputation)
        {
            gradeChamber.text = "5:)";
            return;
        }
        if (reputation < _dialogueBunch.MinReputation)
        {
            gradeChamber.text = "2;(";
            return;
        }
        if (reputation > (_dialogueBunch.MinReputation + _dialogueBunch.MaxReputation) / 2)
        {
            gradeChamber.text = "4";
            return;
        }
        if (reputation > (_dialogueBunch.MinReputation - _dialogueBunch.MaxReputation) / 2)
        {
            gradeChamber.text = "3..";
            return;
        }
    }

    private void SetNewElementAtAnswerBufer(DialogueBaseClass nextDialogueElement, float addReputation)
    {
        CurrentDialogueElement = _dialogueSeter.SetNewElementAtAnswer(nextDialogueElement, addReputation, CurrentDialogueElement);
    }

    private void WritingTextComplition()
    {
        _isWriting = false;
    }

    private void Reseter()
    {
        _simplePhraseChamber.color = Color.black;
        _nameChamber.color = Color.black;
        _gradeChamber.color = Color.red;
        _simplePhraseChamber.text = string.Empty;
        _nameChamber.text = string.Empty;
        _gradeChamber.text = string.Empty;
        foreach (Transform child in _answersChamberTransform)
        {
            _answersChamberLayoutGroup.Buttons.Clear();
            Destroy(child.gameObject);
        }
    }

    ////надо переписать, после вопроса если идет вопрос (те последней до перехода к новому вопросу была простая фраза), закрывается книжка
    //private DialogueBaseClass SetNewElementAtSimplePhrase(List<DialogueBaseClass> dialogue)
    //{
    //    DialogueBaseClass currentDialogueElement = null;
    //    if (CurrentDialogueElement.TypeOfDialogue == TypeOfDialogue.SimplePhrases)
    //    {
    //        if (dialogue.Contains(CurrentDialogueElement))
    //        {
    //            if (dialogue[dialogue.Count - 1] == CurrentDialogueElement && _dialogueBunch.RootDialogue[_dialogueBunch.RootDialogue.Count - 1] != CurrentDialogueElement)
    //            {
    //                //_currentDialogueElement = SetNextElAfterPreviousAnswer(_dialogueBunch.RootDialogue);
    //                //return _currentDialogueElement;
    //            }
    //            currentDialogueElement = dialogue[dialogue.IndexOf(CurrentDialogueElement) + 1];
    //            return currentDialogueElement;
    //        }
    //        //если нам надо спуститься глубже и в данном листе нет того чего мы ищем
    //        for (int i = 0; i < dialogue.Count; i++)
    //        {
    //            if (dialogue[i].TypeOfDialogue == TypeOfDialogue.Answers)
    //            {
    //                foreach (DialogueBaseClass.Answer answer in dialogue[i].Answers)
    //                {
    //                    //??
    //                    currentDialogueElement = SetNewElementAtSimplePhrase(answer.NextDialogueBaseClasses);
    //                    if (currentDialogueElement != null)
    //                    {
    //                        return currentDialogueElement;
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    return null;
    //}

    //public void SetNewElementAtAnswer(DialogueBaseClass currentDialogueElement, float addReputation)
    //{
    //    if(CurrentDialogueElement.TypeOfDialogue == TypeOfDialogue.Answers)
    //    {
    //        //_previousAnswers.Add(_currentDialogueElement);
    //        //SetPreviousAnswers(_dialogueBunch.RootDialogue);

    //        _dialogueBunch.Reputation += addReputation;
    //        CurrentDialogueElement = currentDialogueElement;

    //    }
    //}

    //private void SetPreviousAnswers(List<DialogueBaseClass> dialogue)
    //{
    //    if (CurrentDialogueElement.TypeOfDialogue == TypeOfDialogue.Answers)
    //    {
    //        if (dialogue.Contains(_previousAnswers[_previousAnswers.Count - 2]) && dialogue.Contains(_previousAnswers[_previousAnswers.Count - 1]))
    //        {
    //            _previousAnswers.RemoveAt(_previousAnswers.Count - 2);
    //            return;
    //        }
    //        foreach (DialogueBaseClass el in dialogue)
    //        {
    //            if (el.TypeOfDialogue == TypeOfDialogue.Answers)
    //            {
    //                foreach (DialogueBaseClass.Answer answer in el.Answers)
    //                {
    //                    SetPreviousAnswers(answer.NextDialogueBaseClasses);
    //                }
    //            }
    //        }
    //    }
    //    return;
    //}

    //private DialogueBaseClass SetNextElAfterPreviousAnswer(List<DialogueBaseClass> dialogue)
    //{
    //    if (CurrentDialogueElement.TypeOfDialogue == TypeOfDialogue.SimplePhrases)
    //    {
    //        DialogueBaseClass currentDialogueElement = null;
    //        if (dialogue.Contains(_previousAnswers[_previousAnswers.Count - 1]))
    //        {
    //            if (dialogue[dialogue.Count - 1] != _previousAnswers[_previousAnswers.Count - 1])
    //            {
    //                currentDialogueElement = dialogue[dialogue.IndexOf(_previousAnswers[_previousAnswers.Count - 1]) + 1];
    //                return currentDialogueElement;
    //            }
    //        }
    //        foreach (DialogueBaseClass el in dialogue)
    //        {
    //            if (el.TypeOfDialogue == TypeOfDialogue.Answers)
    //            {
    //                foreach (DialogueBaseClass.Answer answer in el.Answers)
    //                {
    //                    currentDialogueElement = SetNextElAfterPreviousAnswer(answer.NextDialogueBaseClasses);
    //                    if (currentDialogueElement != null)
    //                    {
    //                        return currentDialogueElement;
    //                    }
    //                }
    //            }
    //        }

    //    }
    //    return null;
    //}

}
