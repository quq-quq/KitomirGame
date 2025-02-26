using DG.Tweening;
using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[RequireComponent (typeof(CanvasGroup))]
public class StartForMenu : MonoBehaviour
{
    [SerializeField] private float _delaySeconds;
    [SerializeField] private float _fadeSeconds;
    [SerializeField] private ButtonContainer _buttonContainer; 
    private CanvasGroup _canvasGroup;

    void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(StartDelay());
    }

    private IEnumerator StartDelay()
    {
        _canvasGroup.alpha = 1;
        
        _buttonContainer.gameObject.SetActive(false);
        yield return new WaitForSeconds(_delaySeconds);
        _canvasGroup.DOFade(0, _fadeSeconds);
        _buttonContainer.gameObject.SetActive(true);

    }
}
