using UnityEngine;

public class SecretGameModePlayerVisual : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void UpdateVisual()
    {
        if (SecretGameModePlayer.Instance.IsWalking)
        {
            _animator.SetBool("IsRunning", true);
        }
        else
        {
            _animator.SetBool("IsRunning", false);
        }
    }
}
