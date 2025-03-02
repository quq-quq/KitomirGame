using System.Collections;
using System.Collections.Generic;
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
        [SerializeField, Range(0, 100)] private float _newReputation;
        [SerializeField] private string inputText;
        [SerializeField] private List<DialogueBaseClass> nextDialogueBaseClasses;

        public readonly float NewReputation
        {
            get => _newReputation;
        }
        public readonly string InputText
        {
            get => inputText;
        }
        public readonly List<DialogueBaseClass> NextDialogueBaseClasses
        {
            get => nextDialogueBaseClasses;
        }
    }

    [System.Serializable]
    public struct SimplePhrase
    {
        [SerializeField] private string _inputName;
        [SerializeField, Multiline(6)] private string inputText;

        public readonly string InputName
        {
            get => _inputName;
        }
        public readonly string InputText
        {
            get => inputText;
        }
    }

    [SerializeField] private TypeOfDialogue _typeOfDialogue = TypeOfDialogue.SimplePhrases;
    [SerializeField] private float _symbolTime = 0.1f;
    [SerializeField] private SimplePhrase _simplePhrase;
    [SerializeField] private List<Answer> _answers;

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
}