using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Custom/DialogueBunch"), System.Serializable]
public class DialogueBunch : ScriptableObject
{
    [SerializeField] private bool _isReputationable;
    [HideInInspector] private GameState _nextGameState;
    [HideInInspector] private float _startReputation;
    [HideInInspector] private float _minReputation = 40f;
    [HideInInspector] private float _maxReputation = 85f;
    [Space(20)]
    [SerializeField]private List<DialogueBaseClass> _rootDialogue;
    private float _reputation = 50f;

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
    }

    public void ResetReputation()
    {
        _reputation = _startReputation;
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

            if (GUI.changed)
            {
                EditorUtility.SetDirty(dialogueBunch);
            }
        }
    }
}
