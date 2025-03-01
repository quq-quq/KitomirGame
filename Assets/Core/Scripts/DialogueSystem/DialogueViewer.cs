using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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
    [SerializeField] private ButtonContainer _answersChamberLayoutGroup;
    [SerializeField, Tooltip("must contain AnswerButton script")] private GameObject _answerButtonPrefab;
    
    private Transform _answersChamberTransform;
    private DialogueBaseClass _currentDialogueElement;
    private Coroutine _writingCoroutine;
    private bool _isWriting = false;

    public static bool IsGoing { get; private set; } = false;

    private void Start()
    {
        //if (_answerButtonPrefab == null || _answerButtonPrefab.GetComponent<AnswerButton>() == null)
        //{
        //    Debug.LogError("GameObject dont have AnswerButtonScript or GameObjet dont initialised");
        //}

        if (_isActiveOnStart)
        {
            _dialogueCanvas.gameObject.SetActive(true);
            StartCoroutine(Starter());
        }
        else
        {
            _dialogueCanvas.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (IsGoing && _currentDialogueElement.TypeOfDialogue == TypeOfDialogue.SimplePhrases && Input.anyKeyDown)
        {
            if (_isWriting && _simplePhraseChamber.text.Length > 1)
            {
                _simplePhraseChamber.text = _currentDialogueElement.simplePhrase.InputText;
                _isWriting = false;
                StopCoroutine(_writingCoroutine);
            }
            else
            {
                _currentDialogueElement = SetNewElementAtSimplePhrase(_dialogueBunch.RootDialogue);
                ViewDialogue();
            }

        }
    }

    private IEnumerator Starter()
    {
        _simplePhraseChamber.color = Color.black;
        _simplePhraseChamber.text = "";
        _answersChamberTransform = _answersChamberLayoutGroup.gameObject.transform;
        _simplePhraseChamber.text = string.Empty;
        _nameChamber.text = string.Empty;
        _currentDialogueElement = _dialogueBunch.RootDialogue[0];

        if (!IsGoing)
        {
            yield return new WaitForSeconds(GetCurrentAnim(_dialogueAnimator).length);
            IsGoing = true;
        }

        ViewDialogue();
    }

    private IEnumerator Ender()
    {
        IsGoing = false;
        _dialogueAnimator.SetTrigger("IsEnding");
        yield return new WaitForSeconds(GetCurrentAnim(_dialogueAnimator).length);

        foreach (Transform child in _answersChamberTransform)
        {
            Destroy(child.gameObject);
        }
        _simplePhraseChamber.text = string.Empty;
        _nameChamber.text = string.Empty;
        _dialogueCanvas.gameObject.SetActive(false);
        _currentDialogueElement = _dialogueBunch.RootDialogue[0];
    }

    private void ViewDialogue()
    {
        StopAllCoroutines();
        foreach (Transform child in _answersChamberTransform)
        {
            _answersChamberLayoutGroup.Buttons.Clear();
            Destroy(child.gameObject);
        }
        _simplePhraseChamber.text = string.Empty;
        _nameChamber.text = string.Empty;

        if (_currentDialogueElement != null)
        {
            
            if (_currentDialogueElement.TypeOfDialogue == TypeOfDialogue.SimplePhrases)
            {
                _nameChamber.text = _currentDialogueElement.InputName;
                _writingCoroutine = StartCoroutine(Writer.WritingText(_currentDialogueElement.simplePhrase.InputText, _simplePhraseChamber, _currentDialogueElement.SymbolTime, WritingTextComplition));
                _isWriting = true;
            }
            if (_currentDialogueElement.TypeOfDialogue == TypeOfDialogue.Answers)
            {
                for (int i = 0; i < _currentDialogueElement.Answers.Count; i++)
                {
                    MenuButton currentAnswerButton = Instantiate(_answerButtonPrefab, _answersChamberTransform).GetComponent<MenuButton>();
                    DialogueBaseClass nextDialogueElement = _currentDialogueElement.Answers[i].NextDialogueBaseClasses[0];
                    currentAnswerButton.OnPressMethod.AddListener(() => SetNewElementAtAnswer(nextDialogueElement));

                    //StartCoroutine(DialogueBaseClass.WritingText(_currentDialogueElement.Answers[i].InputText, currentAnswerButton.TextChamber, _currentDialogueElement.SymbolTime));
                    currentAnswerButton.TextChamber.text = _currentDialogueElement.Answers[i].InputText;
                    _answersChamberLayoutGroup.AddButton(currentAnswerButton.TextChamber);
                }
            }
        }
        else
        {
            StartCoroutine(Ender());
        }
    }

    private DialogueBaseClass SetNewElementAtSimplePhrase(List<DialogueBaseClass> dialogue)
    {
        DialogueBaseClass currentDialogueElement = null;
        if (_currentDialogueElement.TypeOfDialogue == TypeOfDialogue.SimplePhrases)
        {
            for (int i = 0; i < dialogue.Count - 1; i++)
            {
                if (dialogue[i] == _currentDialogueElement)
                {
                    currentDialogueElement = dialogue[i + 1];
                    return currentDialogueElement;
                }

                if (dialogue[i].TypeOfDialogue == TypeOfDialogue.Answers)
                {
                    foreach (DialogueBaseClass.Answer answer in dialogue[i].Answers)
                    {
                        currentDialogueElement = SetNewElementAtSimplePhrase(answer.NextDialogueBaseClasses);
                        if (currentDialogueElement != null)
                        {
                            return currentDialogueElement;
                        }
                    }
                }
            }
            for (int i = 0; i < dialogue.Count - 1; i++)
            {
                if (dialogue[i].TypeOfDialogue == TypeOfDialogue.Answers)
                {
                    foreach (DialogueBaseClass.Answer answer in dialogue[i].Answers)
                    {
                        if (answer.NextDialogueBaseClasses.Contains(_currentDialogueElement))
                        {
                            currentDialogueElement = dialogue[i + 1];
                            return currentDialogueElement;
                        }
                    }
                }
            }
        }
        return currentDialogueElement;
    }

    public void SetNewElementAtAnswer(DialogueBaseClass currentDialogueElement)
    {
        if(_currentDialogueElement.TypeOfDialogue == TypeOfDialogue.Answers)
        {
            _currentDialogueElement = currentDialogueElement;
            ViewDialogue();
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
}
