using System;
using UnityEditor;
using UnityEngine;


public class Door : InteractableObject
{
    public static event EventHandler<OnDoorOpenEventArgs> OnDoorOpen;
    public class OnDoorOpenEventArgs : EventArgs
    {
        public string SceneToLoadName;
        public Vector2 DoorPosition;
    }
    
    [SerializeField] private SceneNames _sceneToLoad;
    
    
    public override void Interact()
    {
        OnDoorOpen?.Invoke(this, new OnDoorOpenEventArgs {
            SceneToLoadName = SceneInfo.SceneNamesMap[_sceneToLoad], DoorPosition = transform.position
        });
    }
}
