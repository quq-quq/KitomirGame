using System.Collections;
using UnityEngine;

public class AlarmVisual : MonoBehaviour
{
    [SerializeField] private AnimationClip _wakeUpAnimationClip;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        StartCoroutine(PlayAnimation());
    }

    private IEnumerator PlayAnimation()
    {
        yield return new WaitForSeconds(1.7f);
        _animator.SetBool("IsBlinking", true);
        yield return new WaitForSeconds(_wakeUpAnimationClip.length);
        _animator.SetBool("IsBlinking", false);
    }
}
