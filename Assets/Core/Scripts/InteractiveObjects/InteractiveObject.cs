using System;
using UnityEngine;

public class InteractiveObject : MonoBehaviour {
    
    
    [SerializeField] private GameObject interactSign;
    [SerializeField] private float interactAreaRadius;
    
    [SerializeField] protected bool IsInteractable;

    private void Start() {
        interactSign.SetActive(IsInteractable);
    }
    
    private void Update() {
        float distance = Vector3.Distance(transform.position, Player.Instance.transform.position);
        if (distance <= interactAreaRadius && IsInteractable) {
            interactSign.SetActive(true);
            Player.Instance.SelectInteractiveObject(this);
        }
        else if (distance > interactAreaRadius && IsInteractable) {
            interactSign.SetActive(false);
            Player.Instance.DeselectInteractiveObject(this);
        }
    }

    public virtual void Interact() {
        Debug.Log("Interact");
    }
}
