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
    [System.Serializable]
    public struct Answer
    {
        public string inputText;
        public DialogueBaseClass nextDialogueBaseClass;
    }
    [System.Serializable]
    public struct SimplePhrase
    {
        [SerializeField, Multiline(3)] public string inputText;
    }

    [SerializeField] private float _symbolTime;
    [SerializeField] private SimplePhrase _simplePhrase;
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
