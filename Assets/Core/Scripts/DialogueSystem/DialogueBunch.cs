using System.Collections.Generic;
using UnityEngine;
using static DialogueBunch;

[CreateAssetMenu(menuName = "Custom/DialogueBunch"), System.Serializable]
public class DialogueBunch : ScriptableObject
{
    [SerializeField]private List<DialogueBaseClass> _rootDialogues = new List<DialogueBaseClass>();
    public IReadOnlyList<DialogueBaseClass> RootDialogues
    {
        get => _rootDialogues;
    }
}
