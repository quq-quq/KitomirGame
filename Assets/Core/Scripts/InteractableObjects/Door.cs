using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Door : InteractableObject
{
    public static event EventHandler<OnDoorOpenEventArgs> OnDoorOpen;
    public class OnDoorOpenEventArgs : EventArgs
    {
        public string SceneToLoadName;
        public Vector2 DoorPosition;
    }
    
    [SerializeField] private SceneAsset _sceneToLoad;
    
    
    public override void Interact()
    {
        OnDoorOpen?.Invoke(this, new OnDoorOpenEventArgs {
            SceneToLoadName = _sceneToLoad.name, DoorPosition = transform.position
        });
    }
}
