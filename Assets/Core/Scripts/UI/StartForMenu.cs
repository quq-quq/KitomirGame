using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartForMenu : MonoBehaviour
{
    [System.Serializable]
    private struct Frame
    {
        [SerializeField] public CanvasGroup canvasGroup;
        [Space(10)]
        [SerializeField] public float outFadeTime;
        [SerializeField] public float frameTime;
        [SerializeField] public float fadeTime;
    }

    [SerializeField] private Canvas _canvasButton;
    [Space(10)]
    [SerializeField] private List<Frame> _frames;

    public static event EventHandler OnMenuButtonContainerAppear;
    
    void Start()
    {
        if (GameStateManager.State == GameState.FirstEntry)
        {
            StartCoroutine(StartDelay());
        }
    }

    private IEnumerator StartDelay()
    {
        _canvasButton.gameObject.SetActive(false);

        foreach (var frame in _frames)
        {
            frame.canvasGroup.alpha = 1f;
        }
        
        foreach (Frame frame in _frames)
        {
            frame.canvasGroup.gameObject.SetActive(true);
            yield return frame.canvasGroup.DOFade(1, frame.outFadeTime).WaitForCompletion();
            yield return new WaitForSeconds(frame.frameTime);
            yield return frame.canvasGroup.DOFade(0, frame.fadeTime).WaitForCompletion();
            frame.canvasGroup.gameObject.SetActive(false);
        }

        _canvasButton.gameObject.SetActive(true);
        OnMenuButtonContainerAppear?.Invoke(this, EventArgs.Empty);
    }

}
