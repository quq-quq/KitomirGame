using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    
    [SerializeField] private GameObject _soundSourcePrefab;
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
        GameStateManager.OnStateChanged += GameStateManager_OnStateChanged;

        if (GameStateManager.State == GameState.AtHome)
        {
            PlaySound(_audioClipRefsSO.Alarm, Vector2.zero);
        }
        DialogueViewer.OnCreditBookAction += DialogueViewer_OnCreditBookAction;
        DialogueSetter.OnAnswerAction += DialogueSetter_OnAnswerAction;
        ButtonContainer.OnButtonPressed += ButtonContainer_OnButtonPressed;
        MenuButton.OnPlayButtonPressed += MenuButton_OnPlayButtonPressed;
    }

    private void MenuButton_OnPlayButtonPressed(object sender, EventArgs e)
    {
        PlaySound(_audioClipRefsSO.StartGame, Vector2.zero);
    }

    private void ButtonContainer_OnButtonPressed(object sender, EventArgs e)
    {
        PlaySound(_audioClipRefsSO.ButtonSwitch, Vector2.zero);
    }

    private void DialogueSetter_OnAnswerAction(object sender, DialogueSetter.AnswerActionEventArgs e)
    {
        if (SceneManager.GetActiveScene().name == SceneInfo.CORRIDOR_SCENE) return;
        if (e.isReputationAdded)
        {
            PlaySound(_audioClipRefsSO.Success, Vector2.zero);
        }
        else
        {
            PlaySound(_audioClipRefsSO.Fail, Vector2.zero);
        }
    }

    private void DialogueViewer_OnCreditBookAction(object sender, EventArgs e)
    {
        PlaySound(_audioClipRefsSO.Paper, Player.Instance.transform.position);
    }

    private void BottomLimit_OnItemDropped(object sender, EventArgs e)
    {
        PlaySound(_audioClipRefsSO.RuladaFall, Vector3.zero);
    }

    private void GameStateManager_OnStateChanged(object sender, GameStateManager.OnStateChangedEventArgs e)
    {
        // door sound and commissar appearance
        if (e.CurrentState == GameState.ExamsFailed && Timer.Instance.IsRunning)
        {
            PlaySound(_audioClipRefsSO.CommissarOpenDoor, Vector3.zero);
        }   
    }

    private void Timer_OnAlmostOutOfTime(object sender, EventArgs e)
    {
        PlaySound(_audioClipRefsSO.AlmostOutOfTimeTic, Vector3.zero);
    }

    private void InputManager_OnSecretInputSolved(object sender, EventArgs e)
    {
        PlaySound(_audioClipRefsSO.HardSuccess, Vector3.zero);
    }

    private void FadeScreen_OnWaitAfterFadingStarted(object sender, EventArgs e)
    {
        PlaySound(_audioClipRefsSO.VehicleNoise, Player.Instance.transform.position);
    }

    private void Door_OnDoorOpen(object sender, Door.OnDoorOpenEventArgs e)
    {
        PlaySound(_audioClipRefsSO.OpenDoor, e.DoorPosition);
    }

    private void PlaySound(AudioClip audioClip, Vector2 position, float volume = .4f)
    {
        AudioSource audioSource = Instantiate(_soundSourcePrefab, position, Quaternion.identity)
            .GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        StartCoroutine(DestroySoundSource(audioSource, audioClip.length));
    }

    public void PlayFootstepsSound()
    {
        PlaySound(_audioClipRefsSO.Footsteps, Player.Instance.transform.position);
    }

    private IEnumerator DestroySoundSource(AudioSource soundSource, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(soundSource.gameObject);
    }
    
    private void OnDisable()
    {   
        Door.OnDoorOpen -= Door_OnDoorOpen;
        FadeScreen.OnWaitAfterFadingStarted -= FadeScreen_OnWaitAfterFadingStarted;
        InputManager.Instance.OnSecretInputSolved -= InputManager_OnSecretInputSolved;
        DialogueViewer.OnCreditBookAction -= DialogueViewer_OnCreditBookAction;
        DialogueSetter.OnAnswerAction -= DialogueSetter_OnAnswerAction;
        if (Timer.Instance != null)
        {
            Timer.Instance.OnAlmostOutOfTime -= Timer_OnAlmostOutOfTime;
        }
        if (BottomLimit.Instance != null)
        {
            BottomLimit.Instance.OnItemDropped -= BottomLimit_OnItemDropped;
        }
        GameStateManager.OnStateChanged -= GameStateManager_OnStateChanged;
        ButtonContainer.OnButtonPressed -= ButtonContainer_OnButtonPressed;
        MenuButton.OnPlayButtonPressed -= MenuButton_OnPlayButtonPressed;
    }
}
