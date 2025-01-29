using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Door[] _doorsOnScene;

    private void Start()
    {
        foreach (Door door in _doorsOnScene)
        {
            door.OnDoorOpen += Door_OnDoorOpen;
        }
    }

    private void Door_OnDoorOpen(object sender, Door.OnDoorOpenEventArgs e)
    {
        // load scene with fading
    }
}
