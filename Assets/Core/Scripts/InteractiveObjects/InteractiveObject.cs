using DG.Tweening;
using System;
using UnityEngine;

public class InteractiveObject : MonoBehaviour {
    
    [SerializeField] private SpriteRenderer _interactSignSprite;
    [SerializeField] private float _durationOfSing;
    [SerializeField] private float interactAreaRadius;
    
    private bool _isPlayerInInteractArea = false;

    private void Start()
    {
        _interactSignSprite.color = Color.clear;
    }

    private void Update() {
        float distance = Vector3.Distance(transform.position, Player.Instance.transform.position);
        if (_isPlayerInInteractArea == false && distance <= interactAreaRadius) {
            _isPlayerInInteractArea = true;
            DOTween.KillAll();
            _interactSignSprite.DOColor(Color.white, _interactSignSprite.color.a * _durationOfSing);

            Player.Instance.SelectInteractiveObject(this);
        }
        else if (_isPlayerInInteractArea && distance > interactAreaRadius) {
            _isPlayerInInteractArea = false;
            DOTween.KillAll();
            _interactSignSprite.DOColor(Color.clear, _interactSignSprite.color.a * _durationOfSing);
            Player.Instance.DeselectInteractiveObject(this);
        }
    }

    public virtual void Interact() {
        Debug.Log("Interact");
    }
}
