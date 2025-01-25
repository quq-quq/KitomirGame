using DG.Tweening;
using Dialogue_system;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueViewer[] _dialogueCanvas;
    [SerializeField] private Transform _signE;
    [SerializeField] private float _distanceUp;
    [SerializeField] private float _durationForMove;
    private float _currentDistance;
    private bool _canPressing;
    private int _indexDialogueCanvas;

    private void Awake()
    {
        _currentDistance = _signE.transform.localPosition.y;
    }

    //private void Update()
    //{
    //    if (_canPressing && Input.GetKeyDown(KeyCode.E))
    //    {
    //        _dialogueCanvas[_indexDialogueCanvas].StartDialogue();
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        _anim.SetBool("IsPressing", true);
    //        _canPressing = true;

    //    }
    //}

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && !_canPressing)
        {
            _canPressing = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
        {
            _signE.DOKill();
            _signE.DOLocalMoveY(_currentDistance + _distanceUp, _durationForMove);
            if (_canPressing)
            {
                _dialogueCanvas[_indexDialogueCanvas].StartDialogue();
                _canPressing = false;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
        {
            _signE.DOKill();
            _signE.DOLocalMoveY(_currentDistance, _durationForMove);
            _canPressing = false;
        }
    }

    public void ChangeIndex()
    {
        if(_indexDialogueCanvas+1!= _dialogueCanvas.Length)
            _indexDialogueCanvas += 1;
    }
}
