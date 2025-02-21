using System.Collections.Generic;
using UnityEngine;
using static DialogueBunch;

[CreateAssetMenu(menuName = "Custom/DialogueBunch"), System.Serializable]
public class DialogueBunch : ScriptableObject
{
    [SerializeField]private List<DialogueBaseClass> _rootDialogue = new List<DialogueBaseClass>();
    private int setterId = 0;

    public List<DialogueBaseClass> RootDialogue
    {
        get => _rootDialogue;
    }

    private void OnValidate()
    {
        setterId = 0;
        SetIdForEveryElement(_rootDialogue);
    }

    private void SetIdForEveryElement(List<DialogueBaseClass> dialogue)
    {
        foreach (DialogueBaseClass dialogueElement in dialogue)
        {
            dialogueElement.Id = setterId;
            setterId++;
            //Debug.Log($"Id - {dialogueElement.Id}");
            if (dialogueElement.TypeOfDialogue == TypeOfDialogue.Answers)
            {
                foreach (DialogueBaseClass.Answer answer in dialogueElement.Answers)
                {
                    SetIdForEveryElement(answer.NextDialogueBaseClasses);
                }
            }
        }
    }
}
