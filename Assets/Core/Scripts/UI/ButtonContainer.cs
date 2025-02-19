using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


public class ButtonContainer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _defaultButtons;
    [SerializeField] [CanBeNull] private TextMeshProUGUI[] _hiddenButtons;
    [SerializeField] private GameObject _defaultButtonsGroup;
    [SerializeField] [CanBeNull] private GameObject _hiddenButtonsGroup;
    
    private readonly Color _selectedColorText = Color.black;
    private readonly Color _unselectedColorText = new (0.490566f, 0.490566f, 0.490566f, 1f);
    
    private int _selectedButtonId;
    private TextMeshProUGUI[] _buttons;

    private void Start()
    {
        _selectedButtonId = 0;

        _buttons = _defaultButtons;
        _defaultButtonsGroup.SetActive(true);
        _hiddenButtonsGroup?.SetActive(false);
        
        foreach (var button in _buttons)
        {
            button.color = _unselectedColorText;
        }
        
        SelectButton(_selectedButtonId);

        InputManager.Instance.OnMenuSwitchUp += InputManager_OnMenuSwitchUp;
        InputManager.Instance.OnMenuSwitchDown += InputManager_OnMenuSwitchDown;
        InputManager.Instance.OnMenuButtonPressed += InputManager_OnMenuButtonPressed;
        
        MenuButton.OnHiddenButtonsNeedToSetActive += MenuButton_OnHiddenButtonsNeedToSetActive;
    }

    private void MenuButton_OnHiddenButtonsNeedToSetActive(object sender, EventArgs e)
    {
        ResetActiveButtonsGroup();
    }

    private void InputManager_OnMenuButtonPressed(object sender, EventArgs e)
    {
        UnityEvent unityEvent = _buttons[_selectedButtonId].gameObject.GetComponentInParent<MenuButton>().OnPressMethod;
        if (GameManager.IsGamePaused)
        {
            GameManager.TimeScaleZeroInvoke(unityEvent);
        }
        else
        {
            _buttons[_selectedButtonId].gameObject.GetComponentInParent<MenuButton>().OnPressMethod.Invoke();
        }
    }

    private void InputManager_OnMenuSwitchUp(object sender, EventArgs e)
    {
        
        if (_selectedButtonId > 0)
        {
            DeselectButton(_selectedButtonId);
            _selectedButtonId--;
            SelectButton(_selectedButtonId);
        }
    }

    private void InputManager_OnMenuSwitchDown(object sender, EventArgs e)
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
        if (_buttons[buttonId].text.StartsWith(">"))
        {
            _buttons[buttonId].text = _buttons[buttonId].text.Substring(1, _buttons[buttonId].text.Length - 2);
        }
    }

    private void ResetActiveButtonsGroup()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            DeselectButton(i);
        }
        
        if (_defaultButtonsGroup.activeSelf)
        {
            _defaultButtonsGroup.SetActive(false);
            _hiddenButtonsGroup?.SetActive(true);
            _buttons = _hiddenButtons;
            _selectedButtonId = 0;
            SelectButton(_selectedButtonId);
        }
        else
        {
            _defaultButtonsGroup.SetActive(true);
            _hiddenButtonsGroup?.SetActive(false);
            _buttons = _defaultButtons;
            _selectedButtonId = 0;
            SelectButton(_selectedButtonId);
        }
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnMenuSwitchUp -= InputManager_OnMenuSwitchUp;
        InputManager.Instance.OnMenuSwitchDown -= InputManager_OnMenuSwitchDown;
        InputManager.Instance.OnMenuButtonPressed -= InputManager_OnMenuButtonPressed;
        
        MenuButton.OnHiddenButtonsNeedToSetActive -= MenuButton_OnHiddenButtonsNeedToSetActive;
    }
}
