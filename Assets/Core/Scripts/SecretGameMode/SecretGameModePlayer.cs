using System;
using TMPro;
using UnityEngine;

public class SecretGameModePlayer : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private int _health;
    [SerializeField] private SecretGameModePlayerVisual _visual;

    public static SecretGameModePlayer Instance { get; private set; }
    public event EventHandler OnScoreChanged;
    public event EventHandler OnHealthChanged;
    public event EventHandler OnGameOver;

    private Rigidbody2D _rigidbody2D;
    private Vector2 _movementVector;
    private int _score;
    public bool IsWalking { get; private set; }

    public int Score
    {
        get => _score;
        private set
        {
            _score = value;
            OnScoreChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public int Health
    {
        get => _health;
        private set
        {
            _health = value;
            OnHealthChanged?.Invoke(this, EventArgs.Empty);
        }
    }

private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Score = 0;
    }

    private void FixedUpdate()
    {
        float x = InputManager.Instance.GetMovementVector().x;
        _movementVector = new Vector2(x, 0f);
        IsWalking = _movementVector != Vector2.zero;
        _rigidbody2D.linearVelocity = _movementVector * _moveSpeed;
        _visual.UpdateVisual();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out CollectableItem collectableItem))
        {
            Destroy(collectableItem.gameObject);
            Score++;
        }
    }

    public void LooseHealth()
    {
        Health--;
        if (Health == 0f)
        {
            OnGameOver?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Restart()
    {
        Health = 0;
        Score = 0;
    }
}
