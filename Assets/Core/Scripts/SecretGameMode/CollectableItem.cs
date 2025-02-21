using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [SerializeField] private float _fallingSpeed;
    
    private Rigidbody2D _rigidbody2D;
    private Camera _camera;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
    }

    private void Update()
    {
        _rigidbody2D.linearVelocity = Vector2.down * _fallingSpeed;

        // if (transform.position.y < _camera.ViewportToScreenPoint(new Vector3(1f, 0f, 0f)).y)
        // {
        //     Destroy(gameObject);
        // }
    }
}
