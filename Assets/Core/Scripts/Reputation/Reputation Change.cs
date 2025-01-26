using System;
using Dialogue_system;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReputationChange : MonoBehaviour
{
    
    [SerializeField, Range(0, 1)] private float _startReputation;
    [SerializeField, Range(0, 1)] private float _whenPassed;
    [SerializeField] private bool _ifZeroViewers = false;
    [SerializeField] private Scrollbar _scrollbar;

    private void Start()
    {
        _scrollbar.value = _startReputation;
    }

    private void Update()
    {
        if (_ifZeroViewers)
            return;

        DialogueViewer[] dialogues = FindObjectsOfType<DialogueViewer>();

        Debug.Log(dialogues.Length);
        if (dialogues.Length == 0)
        {
            _ifZeroViewers=true;
            if(_scrollbar.value >= _whenPassed)
            {
                ConfigGameManager.Instance.ExamPassed(SceneManager.GetActiveScene().name);
                Debug.Log("Complete");
            }
            else if (_scrollbar.value < _whenPassed) {
                ConfigGameManager.Instance.ExamFailed();
                Debug.Log("fail");
            }
        }
    }

    public void ChangeReputation(float value)
    {

        _scrollbar.value += value;
    }
}
