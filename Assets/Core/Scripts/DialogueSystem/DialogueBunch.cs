using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

//���� ������� ��� ����� ������������� ��������� ������������ ������ ��� ���������� ������� ����������
[CreateAssetMenu(menuName = "Custom/DialogueBunch"), System.Serializable]
public class DialogueBunch : ScriptableObject
{
    [SerializeField] private bool _isReputationable;
    [SerializeField] private GameState _nextGameState;
    [SerializeField, Range(0, 100)] private float _startReputation;
    [SerializeField, Range(0, 98)] private float _minReputation = 40f;
    [SerializeField, Range(2, 100)] private float _maxReputation = 85f;
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

    //#if UNITY_EDITOR
    //    public void OnValidate()
    //    {
    //        SetTypeParameters(RootDialogue);
    //        UnityEditor.EditorUtility.SetDirty(this);
    //    }
    //#endif

    //    public void SetTypeParameters(List<DialogueBaseClass> dialogue)
    //    {
    //        foreach (DialogueBaseClass el in dialogue)
    //        {
    //            if(el.TypeOfDialogue == TypeOfDialogue.SimplePhrases)
    //            {

    //            }
    //            if (el.TypeOfDialogue == TypeOfDialogue.Answers)
    //            {
    //                foreach (DialogueBaseClass.Answer answer in el.Answers)
    //                {
    //                    SetTypeParameters(answer.NextDialogueBaseClasses);
    //                }
    //            }
    //        }
    //    }
}
