using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Custom/DialogueBunch"), System.Serializable]
public class DialogueBunch : ScriptableObject
{
    [SerializeField] private bool _isReputationable;
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

//        id = 0;
//        Debug.Log("----------------------");
//        SetId(RootDialogue);
//        UnityEditor.EditorUtility.SetDirty(this);
//    }
//#endif

//    public void SetId(List<DialogueBaseClass> dialogue)
//    {
//        foreach(DialogueBaseClass el in dialogue)
//        {
//            el.Id = id;
//            id++;
//            if(el.TypeOfDialogue == TypeOfDialogue.SimplePhrases)
//            {
//                Debug.Log(el.simplePhrase.InputText + " " + id);
//            }
//            if (el.TypeOfDialogue == TypeOfDialogue.Answers)
//            {
//                Debug.Log("ANSW" +" " + id);
//                foreach (DialogueBaseClass.Answer answer in el.Answers)
//                {
//                    SetId(answer.NextDialogueBaseClasses);
//                }
                
//            }
//        }
//    }
}
