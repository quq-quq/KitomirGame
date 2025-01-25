using System;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    private float _musicVolumeFadingTimer = 0f;
    private float _musicVolumeFadingSpeed;
    private AudioSource _audioSource;
    
    private void Start() {
        FadeScreen.Instance.OnFadeStarted += FadeScreen_OnFadeStarted;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        if (_musicVolumeFadingTimer > 0f) {
            _audioSource.volume -= _musicVolumeFadingSpeed * Time.deltaTime;
            _musicVolumeFadingTimer -= Time.deltaTime;
        }
    }

    private void FadeScreen_OnFadeStarted(object sender, FadeScreen.OnFadeStartedEventArgs e) {
        _musicVolumeFadingTimer = e.TotalTime;
        _musicVolumeFadingSpeed = _audioSource.volume/_musicVolumeFadingTimer;
    }
}
