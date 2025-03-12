using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Custom/DialogueBunch"), System.Serializable]
public class DialogueBunch : ScriptableObject
{   
    [Space(20)]
    [SerializeField]private List<DialogueBaseClass> _rootDialogue;
    [Space(70)]
    [Header("Reputation Parameters")]
    [SerializeField, Tooltip("To Activate Reputation System")] private bool _isReputationable = false;
    [SerializeField, Range(0, 100)] private float _startReputation = 60f;
    [SerializeField, Range(0, 97)] private float _minReputation = 40f;
    [SerializeField, Range(3, 100)] private float _maxReputation = 85f;
    [Space(10)]
    [SerializeField] private GameState _nextGameState;
    [Space(10)]
    [SerializeField] private List<string> _necessaryPhrasesForResult;
    [SerializeField] private List<DialogueBaseClass> _goodResultDialogue;
    [SerializeField] private List<DialogueBaseClass> _midResultDialogue;
    [SerializeField] private List<DialogueBaseClass> _badResultDialogue;


    private float _reputation = 50f;
    private List<DialogueBaseClass> _currentDialogue;

    public bool IsReputationable
    {
        get => _isReputationable;
    }
    public GameState NextGameState
    {
        get => _nextGameState;
    }
    public float Reputation
    {
        get => _reputation;
        set => _reputation = Mathf.Clamp(value, 0, 100);
    }
    public float MinReputation
    {
        get => _minReputation;
    }
    public float MaxReputation
    {
        get => _maxReputation;
    }
    public List<DialogueBaseClass> CurrentDialogue
    {
        get => _currentDialogue;
        set
        {
            if (value == _goodResultDialogue || value == _badResultDialogue)
            {
                _currentDialogue = value;
            }
        }
    }
    public List<DialogueBaseClass> GoodResultDialogue
    {
        get => _goodResultDialogue;
    }
    public List<DialogueBaseClass> MidResultDialogue
    {
        get => _midResultDialogue; 
    }
    public List<DialogueBaseClass> BadResultDialogue
    {
        get => _badResultDialogue;
    }
    public List<string> NecessaryPhrasesForResult
    {
        get => _necessaryPhrasesForResult;
    }

    public void ResetBunch()
    {
        _reputation = _startReputation;
        _currentDialogue = _rootDialogue;
    }
}
