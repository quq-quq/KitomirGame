using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.UIElements;
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
    [SerializeField] private VerticalLayoutGroup _answersChamberLayoutGroup;
    [SerializeField, Tooltip("must contain AnswerButton script")] private GameObject _answerButtonPrefab;
    private DialogueBaseClass _currentDialogueElement;
    private Transform _answersChamberTransform;
    private bool _isGoing;
    private int _currentIndex;

    private void Start()
    {
        if (_answerButtonPrefab == null || _answerButtonPrefab.GetComponent<AnswerButton>() == null)
        {
            Debug.LogError("GameObject dont have AnswerButtonScript or GameObjet dont initialised");
        }

        _answersChamberTransform = _answersChamberLayoutGroup.gameObject.transform;
        _simplePhraseChamber.color = Color.black;
        _simplePhraseChamber.text = "";
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

    //как ад листенер в ансверс баттон и ремув листенер с переходом на следующий диалог
    //и как в целом реализовать перепрыгивание между элементами банча
    //через метод в ансвер баттон которые получает значекния надо
    // а обычный текст просто через сет куррент диалог
    //остальсь уничтожение и активация перехода и завершение

    private IEnumerator Starter()
    {
        if (!_isGoing)
        {
            yield return new WaitForSeconds(GetCurrentAnim(_dialogueAnimator).length);
            _isGoing = true;
        }
        _currentDialogueElement = _dialogueBunch.RootDialogue[0];
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
                    currentAnswerButton.NextDialogueClass = _currentDialogueElement.Answers[i].NextDialogueBaseClasses[0];
                    currentAnswerButton.DialogueViewer = this;
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
        foreach (DialogueBaseClass dialogueElement in dialogue)
        {
            if (currentDialogueElement == null)
            {
                if(_currentDialogueElement.TypeOfDialogue == TypeOfDialogue.SimplePhrases)
                {
                    if (_currentDialogueElement.Id + 1 == dialogueElement.Id)
                    {
                        currentDialogueElement = dialogueElement;
                        return currentDialogueElement;
                    }
                    if (dialogueElement.TypeOfDialogue == TypeOfDialogue.Answers)
                    {
                        foreach (DialogueBaseClass.Answer answer in dialogueElement.Answers)
                        {
                            currentDialogueElement = SetNewElementAtSimplePhrase(answer.NextDialogueBaseClasses);
                            if(currentDialogueElement != null)
                            {
                                return currentDialogueElement;
                            }
                        }
                    }
                }

            }
            else break;
        }
        return currentDialogueElement;
    }
    public void SetNewElementAtAnswer(DialogueBaseClass currentDialogueElement)
    {
        _currentDialogueElement = currentDialogueElement;
        ViewDialogue();
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
