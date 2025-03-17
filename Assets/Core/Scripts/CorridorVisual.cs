using UnityEngine;

public class CorridorVisual : MonoBehaviour
{
    [SerializeField] private GameObject _corridorDefault;
    [SerializeField] private GameObject _corridorHappy;

    private void Start()
    {
        if (GameStateManager.State != GameState.ExamsPassed)
        {
            _corridorHappy.gameObject.SetActive(false);
            _corridorDefault.gameObject.SetActive(true);
        }
        else
        {
            _corridorHappy.gameObject.SetActive(true);
            _corridorDefault.gameObject.SetActive(false); 
        }
    }
}
