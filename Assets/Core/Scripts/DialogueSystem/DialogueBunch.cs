using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Custom/DialogueBunch"), System.Serializable]
public class DialogueBunch : ScriptableObject
{
    [SerializeField, Range(0, 100)] float _reputation = 50f;
    [SerializeField, Range(0, 98)] float _minReputation = 40f;
    [SerializeField, Range(2, 100)] float _maxReputation = 85f;
    [SerializeField, Min(0)] float _gradeTypingSpeed;
    [Space(10)]
    [SerializeField]private List<DialogueBaseClass> _rootDialogue;

    public float Reputation
    {
        get => _reputation;
        set
        {
            if (value > 100)
            {
                _reputation = 100;
            }
            else if(value < 0)
            {
                _reputation = 0;
            }
            else
            {
                _reputation = value;
            }
        }
    }
    public float MinReputation
    {
        get => _minReputation;
    }
    public float MaxReputation
    {
        get => _maxReputation;
    }
    public float GradeTypingSpeed
    {
        get => _gradeTypingSpeed;
    }
    public List<DialogueBaseClass> RootDialogue
    {
        get => _rootDialogue;
    }
}
