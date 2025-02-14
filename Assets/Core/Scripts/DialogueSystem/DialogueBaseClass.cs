using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

[System.Serializable]
public class DialogueBaseClass
{
    [SerializeField, Multiline(3)] private string _inputText;
    [SerializeField] private float _symbolTime;
    [SerializeField] private DialogueElement _dialogueElement;

    protected IEnumerator WritingText(string inputText, TextMeshPro textHolder, float symbolTime)
    {
        for(int i = 0; i < inputText.Length; i++)
        {
            textHolder.text += inputText[i];
            yield return new WaitForSeconds(symbolTime);
        }
    }
}
