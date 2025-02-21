using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.UIElements;

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
    private int _currentIndexOfDialogue;
    private bool _isStarted;

    private void Start()
    {
        if (_answerButtonPrefab == null || _answerButtonPrefab.GetComponent<AnswerButton>() == null)
        {
            Debug.LogError("GameObject dont have AnswerButtonScript or GameObjet dont initialised");
        }

        _answersChamberTransform = _answersChamberLayoutGroup.gameObject.transform;
        _simplePhraseChamber.color = Color.black;
        _simplePhraseChamber.text = "";
        _isStarted = false;

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
    //как ад листенер в ансверс баттон и ремув листенер с переходом на следующий диалог
    //и как в целом реализовать перепрыгивание между элементами банча
    //через метод в ансвер баттон которые получает значекния надо
    // а обычный текст просто через сет куррент диалог

    private IEnumerator Starter()
    {
        if (!_isStarted)
        {
            yield return new WaitForSeconds(GetCurrentAnim(_dialogueAnimator).length);
            _isStarted = true;
        }
        ViewDialogue();
    }

    private void ViewDialogue()
    {
        while(_currentDialogueElement != null)
        {
            if (_currentDialogueElement.TypeOfDialogue == TypeOfDialogue.SimplePhrases)
            {
                Coroutine writingText = StartCoroutine(_currentDialogueElement.WritingText(_currentDialogueElement.simplePhrase.InputText, _simplePhraseChamber, _currentDialogueElement.SymbolTime));
            }
            if (_currentDialogueElement.TypeOfDialogue == TypeOfDialogue.Answers)
            {
                for (int i = 0; i < _currentDialogueElement.Answers.Count; i++)
                {
                    AnswerButton currentAnswerButton = Instantiate(_answerButtonPrefab, _answersChamberTransform).GetComponent<AnswerButton>();
                    StartCoroutine(_currentDialogueElement.WritingText(_currentDialogueElement.Answers[i].InputText, currentAnswerButton.TextChamber, _currentDialogueElement.SymbolTime));
                }
            }
        }
    }

    private void SetNextDialogueElement(TypeOfDialogue typeOfDialogue)
    {       
        if (typeOfDialogue == TypeOfDialogue.SimplePhrases)
        {
            bool isFinding = false;
        }
        if(typeOfDialogue == TypeOfDialogue.Answers)
        {
            foreach(GameObject answer in _answersChamberTransform)
            {
                Destroy(answer);
            }
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
