using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Dependencies.Sqlite;
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
        [SerializeField] private string inputText;
        [SerializeField] private List<DialogueBaseClass> nextDialogueBaseClasses;

        public string InputText
        {
            get => inputText;
        }
        public List<DialogueBaseClass> NextDialogueBaseClasses
        {
            get => nextDialogueBaseClasses;
        }
    }

    [System.Serializable]
    public struct SimplePhrase
    {
        [SerializeField, Multiline(3)] private string inputText;

        public string InputText
        {
            get => inputText;
        }
    }

    [SerializeField] private float _symbolTime = 0.1f;
    [SerializeField] private TypeOfDialogue _typeOfDialogue = TypeOfDialogue.SimplePhrases;
    [SerializeField] private SimplePhrase _simplePhrase;
    [SerializeField] private List<Answer> _answers;

    public int Id { get; set; } = -1;
    public float SymbolTime
    {
        get => _symbolTime;
    }
    public TypeOfDialogue TypeOfDialogue
    {
        get => _typeOfDialogue;
    }
    public SimplePhrase simplePhrase
    {
        get => _simplePhrase;
    }
    public List<Answer> Answers
    {
        get => _answers;
    }

    public static IEnumerator WritingText(string inputText, TMP_Text textHolder, float symbolTime)
    {
        if(inputText == null)
        {
            yield break;
        }
        textHolder.text = string.Empty;
        for (int i = 0; i < inputText.Length; i++)
        {
            textHolder.text += inputText[i];
            yield return new WaitForSeconds(symbolTime);
        }
    }

}