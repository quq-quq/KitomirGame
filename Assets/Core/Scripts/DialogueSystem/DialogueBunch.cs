using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/DialogueBunch"), System.Serializable]
public class DialogueBunch : ScriptableObject
{
    public enum TypeOfDialogue
    {
        none,
        Answers,
        SimplePhrases
    }

    [System.Serializable]
    public struct DialogueElement
    {
        public TypeOfDialogue typeOfDialogue;
        public DialogueBaseClass dialogueBaseClass;

    }

    public List<DialogueElement> dialogues = new List<DialogueElement>();
}
