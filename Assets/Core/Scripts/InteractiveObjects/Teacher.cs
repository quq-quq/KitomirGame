using Dialogue_system;
using UnityEngine;

public class Teacher : InteractiveObject {

    [SerializeField] private DialogueViewer _startDialogue;
    public override void Interact() {
        _startDialogue.gameObject.SetActive(true);
    }
}
