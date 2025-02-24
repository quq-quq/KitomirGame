using UnityEngine;

public class BottomLimit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CollectableItem collectableItem))
        {
            Destroy(collectableItem.gameObject);
        }
    }
}
