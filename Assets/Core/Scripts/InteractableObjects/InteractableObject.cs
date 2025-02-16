using UnityEngine;


public class InteractableObject : MonoBehaviour
{
    
    [SerializeField] private GameObject _interactiveSign;
 
    private bool _isInteractable = true;
    
    private void Start()
    {
        _interactiveSign.SetActive(false);
    }

    public virtual void Interact()
    {
        Debug.Log("Parent's Interact");
    }

    public bool TrySelect()
    {
        _interactiveSign.SetActive(true);
        
        return _isInteractable;
    }

    public void Deselect()
    {
        _interactiveSign.SetActive(false);
    }

    public void SetInteractable(bool isInteractable)
    {
        _isInteractable = isInteractable;
    }
}
