using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public enum TypeOfDialogue
{
    SimplePhrases,
    Answers
}

[System.Serializable]
public class DialogueBaseClass
{
    public struct Answer
    {
        public string inputText;
        public DialogueBaseClass nextDialogueBaseClass;
    }

    [SerializeField] private float _symbolTime;
    [SerializeField, Multiline(3)] private string _inputSimpleText;
    [SerializeField] private List<Answer> _answers;

    protected IEnumerator WritingText(string inputText, TextMeshPro textHolder, float symbolTime)
    {
        for(int i = 0; i < inputText.Length; i++)
        {
            textHolder.text += inputText[i];
            yield return new WaitForSeconds(symbolTime);
        }
    }
}
