using DG.Tweening;
using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
[RequireComponent(typeof(RectTransform))]
public class ScrollHandlerText : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]private RectTransform _content;
    [SerializeField] private float _speed, _currentCoroutineTime;
   
    private Vector2 _start , _end;
    private Coroutine _currentCoroutine;
    private ScrollRect _scrollRect;
    private Tween _currentTween;

    private void Awake()
    {
        _scrollRect = GetComponent<ScrollRect>();
        _scrollRect.viewport = gameObject.GetComponent<RectTransform>();
        _scrollRect.content = _content;

        _content.pivot = new Vector2(0.5f , 1);
        _scrollRect.viewport.pivot = new Vector2(0.5f, 1);
        _start = new Vector2(0, 0);
    }

    private void OnEnable()
    {
        StopCurrentCoroutine();

        float duration = Math.Max(_content.rect.height - _scrollRect.viewport.rect.height, 0) / _speed;
        _content.anchoredPosition = _start;
        _currentTween = _content.DOAnchorPosY(Math.Max(_content.rect.height - _scrollRect.viewport.rect.height, 0), duration).SetEase(Ease.Linear);
    }

    private void OnDisable()
    {
        _currentTween.Kill();
        _currentTween= null;
        StopCurrentCoroutine();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _currentTween.Kill();
        _currentTween = null;
        StopCurrentCoroutine();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _currentCoroutine = StartCoroutine(BeforeMovingPause());

        IEnumerator BeforeMovingPause()
        {
            yield return new WaitForSeconds(_currentCoroutineTime);
            float duration = Math.Max(_scrollRect.viewport.anchoredPosition.y -  (_content.anchoredPosition.y - _content.rect.height), 0) / _speed;
            _currentTween = _content.DOAnchorPosY(math.max(_content.rect.height - _scrollRect.viewport.rect.height, 0), duration).SetEase(Ease.Linear);
        }
    }

    private void StopCurrentCoroutine()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }
    }
}
