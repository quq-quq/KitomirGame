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
        public string inputText;
        public List<DialogueBaseClass> nextDialogueBaseClasses;
    }

    [System.Serializable]
    public struct SimplePhrase
    {
        [SerializeField, Multiline(3)] public string inputText;
    }

    [SerializeField] private float _symbolTime;
    [SerializeField] private TypeOfDialogue _typeOfDialogue; // Новое поле для выбора типа диалога
    [SerializeField] private SimplePhrase _simplePhrase;
    [SerializeField] private List<Answer> _answers;

    public TypeOfDialogue TypeOfDialogue => _typeOfDialogue; // Свойство для доступа к типу диалога

    protected IEnumerator WritingText(string inputText, TextMeshPro textHolder, float symbolTime)
    {
        for (int i = 0; i < inputText.Length; i++)
        {
            textHolder.text += inputText[i];
            yield return new WaitForSeconds(symbolTime);
        }
    }
}