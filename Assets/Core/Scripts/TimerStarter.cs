using UnityEngine;

public class TimerStarter : MonoBehaviour
{
    [SerializeField] private GameObject _startDialogue;

    private void Update()
    {
        if (_startDialogue == null)
        {
            Timer.Instance.StartTimer();
            Destroy(this.gameObject);
        }
    }
}
