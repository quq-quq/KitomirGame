using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/DialogueBunch"), System.Serializable]
public class DialogueBunch : ScriptableObject
{
    [SerializeField]private List<DialogueBaseClass> _rootDialogue;

    public List<DialogueBaseClass> RootDialogue
    {
        get => _rootDialogue;
    }
}
