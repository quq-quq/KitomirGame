using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ButtonLogic : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI[] buttonsText;

    private Color _defaultColor;
    private Color _selectedColor;
    private int _selected = 0;

    private void Start() {
        buttonsText[_selected].text = ">" + buttonsText[_selected].text + "<";
        buttonsText[_selected].color = Color.black;
        _defaultColor = buttonsText[_selected+1].color;
        _selectedColor = buttonsText[_selected].color;
    }
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.DownArrow) && _selected < buttonsText.Length - 1) {
            buttonsText[_selected].text = buttonsText[_selected]
                .text.Substring(1, buttonsText[_selected].text.Length - 2);
            buttonsText[_selected].color = _defaultColor;
            _selected++;
            buttonsText[_selected].text = ">" + buttonsText[_selected].text + "<";
            buttonsText[_selected].color = _selectedColor;

        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && _selected > 0) {
            buttonsText[_selected].text = buttonsText[_selected]
                .text.Substring(1, buttonsText[_selected].text.Length - 2);
            buttonsText[_selected].color = _defaultColor;
            _selected--;
            buttonsText[_selected].text = ">" + buttonsText[_selected].text + "<";
            buttonsText[_selected].color = _selectedColor;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) {
            if (_selected == 0) {
                MainMenu.Play();
            }
            else if (_selected == 1) {
                MainMenu.Quit();
            }
        }
    }
}
