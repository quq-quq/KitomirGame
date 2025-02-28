using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerVisual : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
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
    
}
