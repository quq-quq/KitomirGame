using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private AnimationClip _animation;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        if (SceneManager.GetActiveScene().name == SceneNames.KITOMIR_HOME_SCENE)
        {
            StartCoroutine(SleepingAnimation());
        }
    }

    public void UpdateVisual(float x, float y)
    {
        _animator.SetBool("isWalking", Player.Instance.IsMoving);
        if (Player.Instance.IsMoving)
        {
            _animator.SetFloat("x", x);
            _animator.SetFloat("y", y);
        }
    }

    private IEnumerator SleepingAnimation()
    {
        _animator.SetBool("IsSleeping", true);
        Player.Instance.CanAct = false;
        yield return new WaitForSeconds(_animation.length);
        Debug.Log("PIZDA");
        _animator.SetBool("IsSleeping", false);
        Player.Instance.CanAct = true;
    }
}
