using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance { get; private set; }
    
    [SerializeField] private AudioClipRefs audioClipRefs;

    private void Awake() {
        Instance = this;
    }
    
    private void Start() {
        Player.Instance.OnDoorOpened += Player_OnDoorOpened;
        FadeScreen.Instance.OnMotor += FadeScreen_OnMotor;
    }

    private void FadeScreen_OnMotor(object sender, EventArgs e) {
        PlaySound(audioClipRefs.vehicleNoise, Player.Instance.transform.position);
    }

    private void OnDisable() {
        Player.Instance.OnDoorOpened -= Player_OnDoorOpened;
        
    }

    private void Player_OnDoorOpened(object sender, Player.OnDoorOpenEventArgs e) {
        PlaySound(audioClipRefs.openDoor, e.senderTransform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume=1f) {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
    
    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume=1f) {
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    public void PlayFootstepSound() {
        PlaySound(audioClipRefs.footsteps, Player.Instance.transform.position, 3f);
    }
}
