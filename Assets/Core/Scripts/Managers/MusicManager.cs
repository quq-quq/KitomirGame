using System;
using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    
    public void Start()
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

    private void OnDisable()
    {
        FadeScreen.OnFadingStarted -= FadeScreen_OnFadingStarted;
    }
}
