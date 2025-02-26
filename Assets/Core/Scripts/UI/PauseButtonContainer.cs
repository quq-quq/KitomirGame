using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseButtonContainer : ButtonContainer
{
    
    [SerializeField] private List<TextMeshProUGUI> _hiddenButtons;
    [SerializeField] private GameObject _hiddenButtonsGroup;


    protected override void Start()
    {
        
        _hiddenButtonsGroup?.SetActive(false);
        base.Start();
        MenuButton.OnHiddenButtonsNeedToSetActive += MenuButton_OnHiddenButtonsNeedToSetActive;
    }
    
    
    private void MenuButton_OnHiddenButtonsNeedToSetActive(object sender, EventArgs e)
    {
        ResetActiveButtonsGroup();
    }
    
    private void ResetActiveButtonsGroup()
    {
        for (int i = 0; i < Buttons.Count; i++)
        {
            DeselectButton(i);
        }
        
        if (DefaultButtonsGroup.activeSelf)
        {
            DefaultButtonsGroup.SetActive(false);
            _hiddenButtonsGroup.SetActive(true);
            Buttons = _hiddenButtons;
            SelectedButtonId = 0;
            SelectButton(SelectedButtonId);
        }
        else
        {
            DefaultButtonsGroup.SetActive(true);
            _hiddenButtonsGroup.SetActive(false);
            Buttons = DefaultButtons;
            SelectedButtonId = 0;
            SelectButton(SelectedButtonId);
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        MenuButton.OnHiddenButtonsNeedToSetActive -= MenuButton_OnHiddenButtonsNeedToSetActive;
    }
}
