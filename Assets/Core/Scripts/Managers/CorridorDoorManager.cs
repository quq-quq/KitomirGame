//class is no more using in game

using UnityEngine;

public class CorridorDoorManager : MonoBehaviour
{
    [SerializeField] private Door _physicsDoor;
    [SerializeField] private Door _mathsDoor;
    [SerializeField] private Door _iTDoor;
    [SerializeField] private Door _happyEndDoor;
    
    private void Start()
    {
        switch (GameStateManager.State)
        {
            case GameState.PhysicsExam :
                _mathsDoor.gameObject.SetActive(false);
                _iTDoor.gameObject.SetActive(false);
                _happyEndDoor.gameObject.SetActive(false);
                break;
            case GameState.MathsExam : 
                _physicsDoor.gameObject.SetActive(false);
                _iTDoor.gameObject.SetActive(false);
                _happyEndDoor.gameObject.SetActive(false);
                break;
            case GameState.ITExam : 
                _physicsDoor.gameObject.SetActive(false);
                _mathsDoor.gameObject.SetActive(false);
                _happyEndDoor.gameObject.SetActive(false);
                break;
            case GameState.ExamsPassed :
                _physicsDoor.gameObject.SetActive(false);
                _mathsDoor.gameObject.SetActive(false);
                _iTDoor.gameObject.SetActive(false);
                break;
            default : 
                Debug.Log(GameStateManager.State);
                break;
        }
    }
}
