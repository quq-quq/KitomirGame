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
        [HideInInspector]public TypeOfDialogue typeOfDialogue;
        [Multiline]public string inputText;

    }

    [SerializeField] private Animator _animator;
    [SerializeField] private DialogueViewer _dialogueViewer;
    [Space(10)]
    [SerializeField] private List<SetAnimParameter> _setAnimParameters;
    
    private void Update()
    {
        if (!_dialogueViewer.IsCurrentViewerActive() || _setAnimParameters.Count == 0)
        {
            return;
        }
        Deselect();
        foreach( SetAnimParameter setAnimParameter in _setAnimParameters)
        {
            if(setAnimParameter.typeOfDialogue == _dialogueViewer.CurrentDialogueElement.TypeOfDialogue && setAnimParameter.inputText == _dialogueViewer.CurrentDialogueElement.simplePhrase.InputText)
            {
                _animator.SetBool(setAnimParameter.boolParameterName, setAnimParameter.BoolParameterInput);
                _setAnimParameters.Remove(setAnimParameter);
                break;
            }
        }
    }

    public override void Interact()
    {
        if (!DialogueViewer.IsGoing)
        {
            if (_dialogueViewer != null)
                StartCoroutine(_dialogueViewer.Starter());
        }
    }

    public override bool TrySelect()
    {
        if (_isInteractable && _dialogueViewer != null)
        {
            _interactiveSign.SetActive(true);
        }

        return _isInteractable;
    }
}
