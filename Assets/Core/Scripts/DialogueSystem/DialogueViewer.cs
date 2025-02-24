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
    [SerializeField] private VerticalLayoutGroup _answersChamberLayoutGroup;
    [SerializeField, Tooltip("must contain AnswerButton script")] private GameObject _answerButtonPrefab;    
    private Transform _answersChamberTransform;
    private DialogueBaseClass _currentDialogueElement;
    private bool _isGoing;

    private void Start()
    {
        if (_answerButtonPrefab == null || _answerButtonPrefab.GetComponent<AnswerButton>() == null)
        {
            Debug.LogError("GameObject dont have AnswerButtonScript or GameObjet dont initialised");
        }

        _simplePhraseChamber.color = Color.black;
        _simplePhraseChamber.text = "";
        _answersChamberTransform = _answersChamberLayoutGroup.gameObject.transform;
        _currentDialogueElement = _dialogueBunch.RootDialogue[0];
        _isGoing = false;

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
        if (_isGoing && _currentDialogueElement.TypeOfDialogue == TypeOfDialogue.SimplePhrases && (Input.anyKeyDown))
        {
            _currentDialogueElement = SetNewElementAtSimplePhrase(_dialogueBunch.RootDialogue);
            ViewDialogue();
        }
    }

    private IEnumerator Starter()
    {
        if (!_isGoing)
        {
            yield return new WaitForSeconds(GetCurrentAnim(_dialogueAnimator).length);
            _isGoing = true;
        }
        ViewDialogue();
    }

    private IEnumerator Ender()
    {
        _isGoing = false;
        _dialogueAnimator.SetTrigger("IsEnding");
        yield return new WaitForSeconds(GetCurrentAnim(_dialogueAnimator).length);

        foreach (Transform child in _answersChamberTransform)
        {
            Destroy(child.gameObject);
        }
        _simplePhraseChamber.text = string.Empty;
        _dialogueCanvas.gameObject.SetActive(false);
        _currentDialogueElement = _dialogueBunch.RootDialogue[0];
    }

    private void ViewDialogue()
    {
        StopAllCoroutines();
        foreach (Transform child in _answersChamberTransform)
        {
            Destroy(child.gameObject);
        }
        _simplePhraseChamber.text = string.Empty;

        if (_currentDialogueElement != null)
        {
            if (_currentDialogueElement.TypeOfDialogue == TypeOfDialogue.SimplePhrases)
            {
                StartCoroutine(DialogueBaseClass.WritingText(_currentDialogueElement.simplePhrase.InputText, _simplePhraseChamber, _currentDialogueElement.SymbolTime));
            }
            if (_currentDialogueElement.TypeOfDialogue == TypeOfDialogue.Answers)
            {
                for (int i = 0; i < _currentDialogueElement.Answers.Count; i++)
                {
                    AnswerButton currentAnswerButton = Instantiate(_answerButtonPrefab, _answersChamberTransform).GetComponent<AnswerButton>();
                    DialogueBaseClass nextDialogueElement = _currentDialogueElement.Answers[i].NextDialogueBaseClasses[0];
                    currentAnswerButton.Button.onClick.AddListener(() => SetNewElementAtAnswer(nextDialogueElement));

                    StartCoroutine(DialogueBaseClass.WritingText(_currentDialogueElement.Answers[i].InputText, currentAnswerButton.TextChamber, _currentDialogueElement.SymbolTime));
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
}
