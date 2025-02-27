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

    [SerializeField] private ButtonContainer _buttonContainer;
    [Space(10)]
    [SerializeField] private List<Frame> _frames;

    void Start()
    {
        StartCoroutine(StartDelay());
    }

    private IEnumerator StartDelay()
    {
        _buttonContainer.gameObject.SetActive(false);

        foreach (Frame frame in _frames)
        {
            frame.canvasGroup.gameObject.SetActive(true);
            yield return frame.canvasGroup.DOFade(1, frame.outFadeTime).WaitForCompletion();
            yield return new WaitForSeconds(frame.frameTime);
            yield return frame.canvasGroup.DOFade(0, frame.fadeTime).WaitForCompletion();
            frame.canvasGroup.gameObject.SetActive(false);
        }

        _buttonContainer.gameObject.SetActive(true);
    }
}
