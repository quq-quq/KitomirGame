using Dialogue_system;
using System;
using UnityEditor.Build;
using UnityEngine;

public class BunchBus : MonoBehaviour
{
    public static Action<DialogueBunch> StartOrContinueDialogue;
    public static Action<AnswersOfPlayerBunch> StartOrContinueAnswersOfPlayer;

    [SerializeField] private Bunch StartBunch;

    private void Awake()
    {
        if(StartBunch != null)
        {
            if (StartBunch is DialogueBunch dialogueBunch)
            {
                StartOrContinueDialogue?.Invoke(dialogueBunch);
            }

            if (StartBunch is AnswersOfPlayerBunch answersOfPlayerBunch)
            {
                StartOrContinueAnswersOfPlayer?.Invoke(answersOfPlayerBunch);
            }
        }
    }
}
