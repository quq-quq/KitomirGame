using System;
using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    
    public static MusicManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        FadeScreen.OnFadingStarted += FadeScreen_OnFadingStarted;
    }

    private void FadeScreen_OnFadingStarted(object sender, FadeScreen.OnFadingStartedEventArgs e)
    {
        StartCoroutine(Subside(e.FadingDuration));
    }

    private IEnumerator Subside(float duration)
    {
        float defaultVolume = _audioSource.volume;
        
        for (float i = 0; i <= duration; i+=.1f)
        {
            _audioSource.volume = defaultVolume - i/duration;
            yield return new WaitForSeconds(.1f);
        }

        if (_audioSource.volume > 0f)
        {
            _audioSource.volume = 0f;
        }
    }

    public void PauseSoundtrack()
    {
        _audioSource.Pause();
    }

    public void ResumeSoundtrack()
    {
        _audioSource.UnPause();
    }

    private void OnDisable()
    {
        FadeScreen.OnFadingStarted -= FadeScreen_OnFadingStarted;
    }
}
