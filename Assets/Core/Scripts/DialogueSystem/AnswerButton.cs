using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AnswerButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _textChamber;
    private Button _button;

    public DialogueBaseClass NextDialogueClass { private get; set; }
    public DialogueViewer DialogueViewer { private get; set; }
    public TMP_Text TextChamber
    {
        get => _textChamber;
    }

    private void OnEnable()
    {
        _textChamber.color = Color.black;
        _textChamber.text = string.Empty;
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => DialogueViewer.SetNewElementAtAnswer(NextDialogueClass));
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(() => DialogueViewer.SetNewElementAtAnswer(NextDialogueClass));
    }
}
