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
                _mathsDoor.SetInteractable(false);
                _iTDoor.SetInteractable(false);
                _happyEndDoor.SetInteractable(false);
                break;
            case GameState.MathsExam : 
                _iTDoor.SetInteractable(false);
                _happyEndDoor.SetInteractable(false);
                break;
            case GameState.ITExam : 
                _happyEndDoor.SetInteractable(false);
                break;
        }
    }
}
