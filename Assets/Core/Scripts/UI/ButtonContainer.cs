using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


public class ButtonContainer : MonoBehaviour
{
    [SerializeField] protected List<TextMeshProUGUI> DefaultButtons = new List<TextMeshProUGUI>();
    [SerializeField] protected GameObject DefaultButtonsGroup;
    
    private readonly Color _selectedColorText = Color.black;
    private readonly Color _unselectedColorText = new (0.490566f, 0.490566f, 0.490566f, 1f);
    
    protected int SelectedButtonId;
    public List<TextMeshProUGUI> Buttons {  get; protected set; }

    protected virtual void Start()
    {
        SelectedButtonId = 0;

        Buttons = DefaultButtons;
        DefaultButtonsGroup.SetActive(true);
        
        foreach (var button in Buttons)
        {
            button.color = _unselectedColorText;
        }
        if (Buttons.Count > 0)
        {
            SelectButton(SelectedButtonId);
        }

        InputManager.Instance.OnMenuSwitchUp += InputManager_OnMenuSwitchUp;
        InputManager.Instance.OnMenuSwitchDown += InputManager_OnMenuSwitchDown;
        InputManager.Instance.OnMenuButtonPressed += InputManager_OnMenuButtonPressed;
    }


    private void InputManager_OnMenuButtonPressed(object sender, EventArgs e)
    {
        UnityEvent unityEvent = Buttons[SelectedButtonId].gameObject.GetComponentInParent<MenuButton>().OnPressMethod;
        if (GameManager.IsGamePaused)
        {
            GameManager.TimeScaleZeroInvoke(unityEvent);
        }
        else
        {
            Buttons[SelectedButtonId].gameObject.GetComponentInParent<MenuButton>().OnPressMethod.Invoke();
        }
    }

    private void InputManager_OnMenuSwitchUp(object sender, EventArgs e)
    {
        
        if (SelectedButtonId > 0)
        {
            DeselectButton(SelectedButtonId);
            SelectedButtonId--;
            SelectButton(SelectedButtonId);
        }
    }

    private void InputManager_OnMenuSwitchDown(object sender, EventArgs e)
    {
        if (SelectedButtonId < Buttons.Count - 1)
        {
            DeselectButton(SelectedButtonId);
            SelectedButtonId++;
            SelectButton(SelectedButtonId);
        }
    }

    protected void SelectButton(int buttonId)
    {
        Buttons[buttonId].color = _selectedColorText;
        Buttons[buttonId].text = ">" + Buttons[buttonId].text + "<";
    }

    protected void DeselectButton(int buttonId)
    {
        Buttons[buttonId].color = _unselectedColorText;
        if (Buttons[buttonId].text.StartsWith(">"))
        {
            Buttons[buttonId].text = Buttons[buttonId].text.Substring(1, Buttons[buttonId].text.Length - 2);
        }
    }

    public void AddButton(TextMeshProUGUI buttonText)
    {
        Buttons.Add(buttonText);
        if (Buttons.Count == 1)
        {
            SelectedButtonId = 0;
            SelectButton(0);
        }
    }

    protected virtual void OnDestroy()
    {
        InputManager.Instance.OnMenuSwitchUp -= InputManager_OnMenuSwitchUp;
        InputManager.Instance.OnMenuSwitchDown -= InputManager_OnMenuSwitchDown;
        InputManager.Instance.OnMenuButtonPressed -= InputManager_OnMenuButtonPressed;
    }
}
