using System;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{


    [SerializeField] private GameObject interactSign;
    [SerializeField] private float interactAreaRadius;

    private bool _isPlayerInInteractArea = false;
    [SerializeField] protected bool IsInteractable = false;

    private void Start()
    {
        interactSign.SetActive(false);
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.Instance.transform.position);
        if (_isPlayerInInteractArea == false && distance <= interactAreaRadius && IsInteractable)
        {
            _isPlayerInInteractArea = true;
            interactSign.SetActive(true);
            Player.Instance.SelectInteractiveObject(this);
        }
        else if (_isPlayerInInteractArea && distance > interactAreaRadius && IsInteractable)
        {
            _isPlayerInInteractArea = false;
            interactSign.SetActive(false);
            Player.Instance.DeselectInteractiveObject(this);
        }
    }

    public virtual void Interact()
    {
        Debug.Log("Interact");
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Устанавливаем цвет гизмо
        Gizmos.DrawWireSphere(transform.position, interactAreaRadius); // Рисуем круг
    }
}
