using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Custom/DialogueBunch"), System.Serializable]
public class DialogueBunch : ScriptableObject
{
    [SerializeField] private bool _isReputationable;
    private GameState _nextGameState;
    private float _startReputation;
    private float _minReputation = 40f;
    private float _maxReputation = 85f;
    
    [Space(20)]
    [SerializeField]private List<DialogueBaseClass> _rootDialogue;
    [Space(20)]
    [SerializeField] private List<string> _necessaryPhrasesForResult;
    [SerializeField] private List<DialogueBaseClass> _goodResultDialogue;
    [SerializeField] private List<DialogueBaseClass> _badResultDialogue;
    private float _reputation = 50f;
    private bool _canResult = false;

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
    public List<DialogueBaseClass> RootDialogue
    {
        get => _rootDialogue;
        set
        {
            if (value == _goodResultDialogue || value == _badResultDialogue)
            {
                _rootDialogue = value;
            }
        }
    }
    public List<DialogueBaseClass> GoodResultDialogue
    {
        get => _goodResultDialogue;
    }
    public List<DialogueBaseClass> BadResultDialogue
    {
        get => _badResultDialogue;
    }

    public void ResetReputation()
    {
        _reputation = _startReputation;
    }

    public bool CanResulting(DialogueBaseClass currentEl)
    {
        if (!_canResult)
        {
            _canResult = _necessaryPhrasesForResult.Contains(currentEl.simplePhrase.InputText);
            return _canResult;
        }
        return true;
    }

    [CustomEditor(typeof(DialogueBunch))]
    public class DialogueBunchEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DialogueBunch dialogueBunch = (DialogueBunch)target;

            if (dialogueBunch._isReputationable)
            {
                EditorGUILayout.Space();
                dialogueBunch._nextGameState = (GameState)EditorGUILayout.EnumPopup("Next Game State", dialogueBunch._nextGameState);
                dialogueBunch._startReputation = EditorGUILayout.Slider("Start Reputation", dialogueBunch._startReputation, 0, 100);
                dialogueBunch._minReputation = EditorGUILayout.Slider("Min Reputation", dialogueBunch._minReputation, 0, 98);
                dialogueBunch._maxReputation = EditorGUILayout.Slider("Max Reputation", dialogueBunch._maxReputation, 2, 100);
            }

            EditorGUILayout.Space();
            DrawDefaultInspector();
            EditorGUILayout.Space();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(dialogueBunch);
            }
        }
    }
}
