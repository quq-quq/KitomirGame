using UnityEngine;

[CreateAssetMenu()]
public class AudioClipRefs : ScriptableObject {
    
    public AudioClip[] regularFail;
    public AudioClip[] hardFail;
    public AudioClip[] success;
    public AudioClip[] hardSuccess;
    public AudioClip[] footsteps;
    public AudioClip alarm;
    public AudioClip alarmOff;
    public AudioClip blanket;
    public AudioClip[] openDoor;
    public AudioClip[] vehicleNoise;
}
