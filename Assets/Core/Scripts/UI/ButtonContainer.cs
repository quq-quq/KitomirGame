using System;
using System.Collections.Generic;
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

    private void Start()
    {
        InitializeContainer();
        
        InputManager.Instance.OnMenuSwitchUp += InputManager_OnMenuSwitchUp;
        InputManager.Instance.OnMenuSwitchDown += InputManager_OnMenuSwitchDown;
        InputManager.Instance.OnMenuButtonPressed += InputManager_OnMenuButtonPressed;
    }


    private void InputManager_OnMenuButtonPressed(object sender, EventArgs e)
    {
        if (!GameManager.IsGamePaused)
        {
            PressButton();
        }
    }

    private void InputManager_OnMenuSwitchUp(object sender, EventArgs e)
    {
        if (!GameManager.IsGamePaused)
        {
            SwitchButtonUp();
        }
    }

    private void InputManager_OnMenuSwitchDown(object sender, EventArgs e)
    {
        if (!GameManager.IsGamePaused)
        {
            SwitchButtonDown();
        }
    }

    protected void PressButton()
    {
        if (Buttons.Count == 0) return;
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

    protected void SwitchButtonUp()
    {
        if (SelectedButtonId > 0)
        {
            DeselectButton(SelectedButtonId);
            SelectedButtonId--;
            SelectButton(SelectedButtonId);
        }
    }

    protected void SwitchButtonDown()
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

        for (int i = 0; i < Buttons.Count; i++)
        {
            if (i != SelectedButtonId)
            {
                DeselectButton(i);
            }
        }
    }

    protected void InitializeContainer()
    {
        SelectedButtonId = 0;

        Buttons = DefaultButtons;
        DefaultButtonsGroup.SetActive(true);
        var buttonsToRemove = new List<TextMeshProUGUI>();
        
        foreach (var button in Buttons)
        {
            if (!button.IsActive())
            {
                buttonsToRemove.Add(button);
            }
            button.color = _unselectedColorText;
        }
        foreach (var button in buttonsToRemove)
        {
            Buttons.Remove(button);
        }
        
        if (Buttons.Count > 0)
        {
            SelectButton(SelectedButtonId);
        }
    }

    protected virtual void OnDestroy()
    {
        InputManager.Instance.OnMenuSwitchUp -= InputManager_OnMenuSwitchUp;
        InputManager.Instance.OnMenuSwitchDown -= InputManager_OnMenuSwitchDown;
        InputManager.Instance.OnMenuButtonPressed -= InputManager_OnMenuButtonPressed;
    }
}
