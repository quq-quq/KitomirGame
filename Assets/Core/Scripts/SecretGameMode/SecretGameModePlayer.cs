using UnityEngine;

public class SecretGameModePlayer : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private int _health;
    
    private Rigidbody2D _rigidbody2D;
    private Vector2 _movementVector;
    private int _score;
    
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float x = InputManager.Instance.GetMovementVector().x;
        x = (x > 0f) ? 1f : (x < 0f) ? -1f : 0f;
        _movementVector = new Vector2(x, 0f);
        _rigidbody2D.linearVelocity = _movementVector * _moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out CollectableItem collectableItem))
        {
            Destroy(collectableItem.gameObject);
            _score++;
        }
    }

    public void LooseHealth()
    {
        _health--;
        if (_health == 0f)
        {
            //game over
        }
    }
}
