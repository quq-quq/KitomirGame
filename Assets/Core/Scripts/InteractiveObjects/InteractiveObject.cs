using System;
using UnityEngine;

public class InteractiveObject : MonoBehaviour {
    
    
    [SerializeField] private GameObject interactSign;
    [SerializeField] private float interactAreaRadius;
    
    private bool _isPlayerInInteractArea = false;
    private bool _isInteractable = true;

    private void Start() {
        interactSign.SetActive(false);
    }
    
    private void Update() {
        float distance = Vector3.Distance(transform.position, Player.Instance.transform.position);
        if (_isPlayerInInteractArea == false && distance <= interactAreaRadius && _isInteractable) {
            _isPlayerInInteractArea = true;
            interactSign.SetActive(true);
            Player.Instance.SelectInteractiveObject(this);
        }
        else if (_isPlayerInInteractArea && distance > interactAreaRadius && _isInteractable) {
            _isPlayerInInteractArea = false;
            interactSign.SetActive(false);
            Player.Instance.DeselectInteractiveObject(this);
        }
    }

    public virtual void Interact() {
        Debug.Log("Interact");
    }

    public void SetInteractable(bool value) {
        _isInteractable = value;
    }
}
