using System.Collections.Generic;
using UnityEngine;

namespace Dialogue_system
{
    [CreateAssetMenu(fileName = "New Dialogue Bunch", menuName = "Dialogue Bunch", order = -1)]
    public class DialogueBunch : ScriptableObject
    {
        [SerializeField] private List<Dialogue> _dialogues;

        public IReadOnlyList<Dialogue> Dialogues
        {
            get => _dialogues;
        }
    }
}
