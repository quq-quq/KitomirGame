using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Door : InteractableObject
{
    public event EventHandler<OnDoorOpenEventArgs> OnDoorOpen;
    public class OnDoorOpenEventArgs : EventArgs
    {
        public string SceneToLoadName;
    }
    
    [SerializeField] private Scene _sceneToLoad;
    
    
    public override void Interact()
    {
        
        Debug.Log("Door Interact");
        OnDoorOpen?.Invoke(this, new OnDoorOpenEventArgs {
            SceneToLoadName = _sceneToLoad.name
        });
    }
}
