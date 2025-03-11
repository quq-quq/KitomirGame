using System;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        if (BottomLimit.Instance != null)
        {
            BottomLimit.Instance.OnItemDropped += BottomLimit_OnItemDropped;
        }

        // if (SecretGameModePlayer.Instance != null)
        // {
        //     SecretGameModePlayer.Instance.OnGameOver += SecretGameModePlayer_OnGameOver;
        // }
        GameStateManager.OnStateChanged += GameStateManager_OnStateChanged;

        if (GameStateManager.State == GameState.AtHome)
        {
            PlaySound(_audioClipRefsSO.alarm, Vector2.zero);
        }
    }

    // private void SecretGameModePlayer_OnGameOver(object sender, EventArgs e)
    // {
    //     PlaySound(_audioClipRefsSO.hardFail, Vector3.zero);
    // }

    private void BottomLimit_OnItemDropped(object sender, EventArgs e)
    {
        PlaySound(_audioClipRefsSO.ruladaFall, Vector3.zero);
    }

    private void GameStateManager_OnStateChanged(object sender, GameStateManager.OnStateChangedEventArgs e)
    {
        if (e.CurrentState == GameState.ExamsFailed && Timer.Instance.IsRunning)
        {
            PlaySound(_audioClipRefsSO.commissarOpenDoor, Vector3.zero);
        }   
    }

    private void Timer_OnAlmostOutOfTime(object sender, EventArgs e)
    {
        PlaySound(_audioClipRefsSO.almostOutOfTimeTic, Vector3.zero);
    }

    private void InputManager_OnSecretInputSolved(object sender, EventArgs e)
    {
        PlaySound(_audioClipRefsSO.hardSuccess, Vector3.zero);
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
        if (BottomLimit.Instance != null)
        {
            BottomLimit.Instance.OnItemDropped -= BottomLimit_OnItemDropped;
        }
        GameStateManager.OnStateChanged -= GameStateManager_OnStateChanged;
    }
}
