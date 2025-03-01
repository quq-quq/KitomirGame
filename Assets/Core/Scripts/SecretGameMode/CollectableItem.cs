using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    
    private Rigidbody2D _rigidbody2D;
    private float _fallingSpeed;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _rigidbody2D.linearVelocity = Vector2.down * _fallingSpeed;

        // if (transform.position.y < _camera.ViewportToScreenPoint(new Vector3(1f, 0f, 0f)).y)
        // {
        //     Destroy(gameObject);
        // }
    }

    public void SetFallingSpeed(float fallingSpeedMin, float fallingSpeedMax)
    {
        _fallingSpeed = Random.Range(fallingSpeedMin, fallingSpeedMax);
    }
}
