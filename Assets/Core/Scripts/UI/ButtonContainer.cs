using System;
using TMPro;
using UnityEngine;


public class ButtonContainer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _buttons;

    private readonly Color _selectedColorText = Color.black;
    private readonly Color _unselectedColorText = new (0.490566f, 0.490566f, 0.490566f, 1f);
    
    private int _selectedButtonId;

    private void Start()
    {
        _selectedButtonId = 0;

        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].color = _unselectedColorText;
        }
        
        SelectButton(_selectedButtonId);
    
        InputManager.Instance.OnMainMenuSwitchDown += InputManager_MainMenuSwitchDown;
        InputManager.Instance.OnMainMenuSwitchUp += InputManager_MainMenuSwitchUp;
        InputManager.Instance.OnMainMenuButtonPressed += InputManager_MainMenuButtonPressed;
    }

    private void InputManager_MainMenuButtonPressed(object sender, EventArgs e)
    {
        _buttons[_selectedButtonId].gameObject.GetComponentInParent<MenuButton>().onPressMethod.Invoke();
    }

    private void InputManager_MainMenuSwitchUp(object sender, EventArgs e)
    {
        if (_selectedButtonId > 0)
        {
            DeselectButton(_selectedButtonId);
            _selectedButtonId--;
            SelectButton(_selectedButtonId);
        }
    }

    private void InputManager_MainMenuSwitchDown(object sender, EventArgs e)
    {
        if (_selectedButtonId < _buttons.Length - 1)
        {
            DeselectButton(_selectedButtonId);
            _selectedButtonId++;
            SelectButton(_selectedButtonId);
        }
    }

    private void SelectButton(int buttonId)
    {
        _buttons[buttonId].color = _selectedColorText;
        _buttons[buttonId].text = ">" + _buttons[buttonId].text + "<";
    }

    private void DeselectButton(int buttonId)
    {
        _buttons[buttonId].color = _unselectedColorText;
        _buttons[buttonId].text = _buttons[buttonId].text.Substring(1, _buttons[buttonId].text.Length - 2);
    }

    private void OnDisable()
    {
        InputManager.Instance.OnMainMenuSwitchDown -= InputManager_MainMenuSwitchDown;
        InputManager.Instance.OnMainMenuSwitchUp -= InputManager_MainMenuSwitchUp;
        InputManager.Instance.OnMainMenuButtonPressed -= InputManager_MainMenuButtonPressed;
    }
}
