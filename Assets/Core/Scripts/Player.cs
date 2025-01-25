using System;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float moveSpeed = 5f;

    public static Player Instance { get; private set; }
    
    private InteractiveObject _selectedInteractiveObject;
    

    private void Awake() {
        Instance = this;
        
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e) {
        _selectedInteractiveObject?.Interact();
    }

    private void Update() {
        HandleMovement();
    }

    private void HandleMovement() {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, inputVector.y, 0);
        
        transform.position += moveDirection * (moveSpeed * Time.deltaTime);
    }

    public void SelectInteractiveObject(InteractiveObject interactiveObject) {
        if (_selectedInteractiveObject is null) {
            _selectedInteractiveObject = interactiveObject;
        }
    }

    public void DeselectInteractiveObject(InteractiveObject interactiveObject) {
        if (_selectedInteractiveObject == interactiveObject) {
            _selectedInteractiveObject = null;
        }
    }
}
