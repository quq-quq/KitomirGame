using System.Collections.Generic;
using UnityEngine;
using static DialogueBunch;

[CreateAssetMenu(menuName = "Custom/DialogueBunch"), System.Serializable]
public class DialogueBunch : ScriptableObject
{
    public List<DialogueBaseClass> rootDialogues = new List<DialogueBaseClass>();
}
