using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private GameObject _interactiveSign;
    
    private void Start()
    {
        _interactiveSign.SetActive(false);
    }

    public virtual void Interact()
    {
        Debug.Log("Parent's Interact");
    }

    public void Select()
    {
        _interactiveSign.SetActive(true);
    }

    public void Deselect()
    {
        _interactiveSign.SetActive(false);
    }
}
