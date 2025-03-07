using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(DialogueViewer))]
public class TriggerSpeech : InteractableObject
{
    [Space(30)]
    [SerializeField] GameState _nessecaryState;
    private Collider2D collider2D;
    private DialogueViewer _dialogueViewer;

    private void Start()
    {
        collider2D = GetComponent<Collider2D>();
        _dialogueViewer = GetComponent<DialogueViewer>();
        collider2D.isTrigger = true;
    }

    public override bool TrySelect()
    {
        if (_isInteractable && GameStateManager.State == _nessecaryState)
        {
            Interact();
        }
        return _isInteractable;
    }

    public override void Interact()
    {
        StartCoroutine(_dialogueViewer.Starter());
    }

    public override void Deselect()
    {
        return;
    }
}
