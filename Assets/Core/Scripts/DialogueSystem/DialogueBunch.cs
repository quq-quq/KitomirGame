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
    private int id = 0;

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
