using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AnswerButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _textChamber;

    public TMP_Text TextChamber
    {
        get => _textChamber;
    }

    private Button _button;

    private void OnEnable()
    {
        _textChamber.color = Color.black;
        _textChamber.text = "";
        _button = GetComponent<Button>();
        //_button?.onClick.AddListener();
    }
}
