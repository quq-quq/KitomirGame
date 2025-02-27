using System;
using UnityEngine;

public class FinalPauseVisual : MonoBehaviour
{
    [SerializeField] private GameObject _warningText;
    [SerializeField] private GameObject _afterFinalSoundtrackText;

    private void Start()
    {
        _warningText.SetActive(true);
        _afterFinalSoundtrackText.SetActive(false);
        MusicManager.OnHappySoundtrackFinished += MusicManager_OnHappySoundtrackFinished;
    }

    private void MusicManager_OnHappySoundtrackFinished(object sender, EventArgs e)
    {
        Debug.Log("ya ebu");
        _afterFinalSoundtrackText.SetActive(true);
        _warningText.SetActive(false);
    }

    private void OnDisable()
    {
        MusicManager.OnHappySoundtrackFinished -= MusicManager_OnHappySoundtrackFinished;
    }
}
