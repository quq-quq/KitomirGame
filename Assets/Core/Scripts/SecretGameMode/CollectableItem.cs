using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectableItem : MonoBehaviour
{
    [SerializeField] private float _rotationDurationMax; 
    [SerializeField] private float _rotationDurationMin;
    
    private Rigidbody2D _rigidbody2D;
    private float _fallingSpeed;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        float rotationDuration = Random.Range(_rotationDurationMin, _rotationDurationMax);
        int direction = Random.Range(0, 2) == 0 ? -1 : 1;
        gameObject.transform.DORotate(new Vector3(0, 0, 360 * direction), rotationDuration, RotateMode.LocalAxisAdd)
            .SetLoops(3, LoopType.Restart).SetEase(Ease.Linear);
    }

    private void Update()
    {
        _rigidbody2D.linearVelocity = Vector2.down * _fallingSpeed;
    }

    public void SetFallingSpeed(float fallingSpeedMin, float fallingSpeedMax)
    {
        _fallingSpeed = Random.Range(fallingSpeedMin, fallingSpeedMax);
    }

    private void OnDestroy()
    { 
        gameObject.transform.DOKill(); 
    }
}
