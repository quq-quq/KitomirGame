using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    
    [SerializeField] private AudioClipRefsSO _audioClipRefsSO;

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
        Door.OnDoorOpen += Door_OnDoorOpen;
        FadeScreen.OnWaitAfterFadingStarted += FadeScreen_OnWaitAfterFadingStarted;
        InputManager.Instance.OnSecretInputSolved += InputManager_OnSecretInputSolved;
        if (Timer.Instance != null)
        {
            Timer.Instance.OnAlmostOutOfTime += Timer_OnAlmostOutOfTime;
        }
        GameStateManager.OnStateChanged += GameStateManager_OnStateChanged;
    }

    private void GameStateManager_OnStateChanged(object sender, GameStateManager.OnStateChangedEventArgs e)
    {
        if (GameStateManager.State == GameState.ExamsFailed)
        {
            PlaySound(_audioClipRefsSO.hardFail, Vector3.zero);
        }
    }

    private void Timer_OnAlmostOutOfTime(object sender, EventArgs e)
    {
        PlaySound(_audioClipRefsSO.almostOutOfTimeTic, Vector3.zero);
    }

    private void InputManager_OnSecretInputSolved(object sender, EventArgs e)
    {
        PlaySound(_audioClipRefsSO.success, Vector3.zero);
    }

    private void FadeScreen_OnWaitAfterFadingStarted(object sender, EventArgs e)
    {
        PlaySound(_audioClipRefsSO.vehicleNoise, Vector2.zero);
    }

    private void Door_OnDoorOpen(object sender, Door.OnDoorOpenEventArgs e)
    {
        PlaySound(_audioClipRefsSO.openDoor, e.DoorPosition);
    }

    private void PlaySound(AudioClip audioClip, Vector2 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
    
    private void PlaySound(AudioClip[] audioClips, Vector2 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClips[Random.Range(0, audioClips.Length)], position, volume);
    }

    public void PlayFootstepsSound()
    {
        PlaySound(_audioClipRefsSO.footsteps, Player.Instance.transform.position);
    }

    private void OnDisable()
    {
        Door.OnDoorOpen -= Door_OnDoorOpen;
        FadeScreen.OnWaitAfterFadingStarted -= FadeScreen_OnWaitAfterFadingStarted;
        InputManager.Instance.OnSecretInputSolved -= InputManager_OnSecretInputSolved;
        if (Timer.Instance != null)
        {
            Timer.Instance.OnAlmostOutOfTime -= Timer_OnAlmostOutOfTime;
        }
    }
}
