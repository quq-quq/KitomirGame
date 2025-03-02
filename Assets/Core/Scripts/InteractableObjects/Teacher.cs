using System.Collections.Generic;
using UnityEngine;


public class Teacher : InteractableObject
{
    [System.Serializable]
    private struct SetAnimParameter
    {
        public string boolParameterName;
        public bool BoolParameterInput;
        [Space(10)]
        public TypeOfDialogue typeOfDialogue;
        [Multiline]public string inputText;

    }

    [SerializeField] private Animator _animator;
    [SerializeField] private DialogueViewer _dialogueViewer;
    [Space(10)]
    [SerializeField] private List<SetAnimParameter> _setAnimParameters;

    private void Update()
    {
        if (!DialogueViewer.IsGoing || _setAnimParameters.Count == 0)
        {
            Debug.Log("2");
            return;
        }
        else
        {
            foreach( SetAnimParameter setAnimParameter in _setAnimParameters)
            {
                if(setAnimParameter.typeOfDialogue == _dialogueViewer.CurrentDialogueElement.TypeOfDialogue && setAnimParameter.inputText == _dialogueViewer.CurrentDialogueElement.simplePhrase.InputText)
                {
                    Debug.Log("1");
                    _animator.SetBool(setAnimParameter.boolParameterName, setAnimParameter.BoolParameterInput);
                }
            }
        }
    }

    public override void Interact()
    {
        Debug.Log("Teacher Interact");
    }
}
