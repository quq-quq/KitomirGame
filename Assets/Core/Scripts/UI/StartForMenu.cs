using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartForMenu : MonoBehaviour
{
    [Serializable]
    private struct Frame
    {
        [SerializeField] public CanvasGroup canvasGroup;
        [Space(10)]
        [SerializeField] public float outFadeTime;
        [SerializeField] public float frameTime;
        [SerializeField] public float fadeTime;
    }

    [SerializeField] private ButtonContainer _mainButtonContainer;
    [Space(10)]
    [SerializeField] private List<Frame> _frames;

    public static event EventHandler OnMenuButtonContainerAppear;
    
    void Start()
    {
        if (GameStateManager.State == GameState.FirstEntry)
        {
            StartCoroutine(StartDelay());
        }
        MenuButton.OnShowInfoPressed += MenuButton_OnShowInfoPressed;
        MenuButton.OnHideInfoPressed += MenuButton_OnHideInfoPressed;
    }

    private void MenuButton_OnHideInfoPressed(object sender, EventArgs e)
    {
        _mainButtonContainer.gameObject.SetActive(true);
    }

    private void MenuButton_OnShowInfoPressed(object sender, EventArgs e)
    {
        _mainButtonContainer.gameObject.SetActive(false);
    }

    private IEnumerator StartDelay()
    {
        _mainButtonContainer.gameObject.SetActive(false);

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

        _mainButtonContainer.gameObject.SetActive(true);
        OnMenuButtonContainerAppear?.Invoke(this, EventArgs.Empty);
    }

    private void OnDisable()
    {
        MenuButton.OnShowInfoPressed -= MenuButton_OnShowInfoPressed;
        MenuButton.OnHideInfoPressed -= MenuButton_OnHideInfoPressed;
    }
}
