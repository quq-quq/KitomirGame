using System;
using UnityEngine;

public class BottomLimit : MonoBehaviour
{
    public static BottomLimit Instance { get; private set; }
    public event EventHandler OnItemDropped;

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
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CollectableItem collectableItem))
        {
            Destroy(collectableItem.gameObject);
            OnItemDropped?.Invoke(this, EventArgs.Empty);
            SecretGameModePlayer.Instance.LooseHealth();
        }
    }
}
