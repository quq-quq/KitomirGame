using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Door : InteractableObject
{
    public event EventHandler<OnDoorOpenEventArgs> OnDoorOpen;
    public class OnDoorOpenEventArgs : EventArgs
    {
        public string SceneToLoadName;
    }
    
    [SerializeField] private SceneAsset _sceneToLoad;
    
    
    public override void Interact()
    {
        OnDoorOpen?.Invoke(this, new OnDoorOpenEventArgs {
            SceneToLoadName = _sceneToLoad.name
        });
    }
}
