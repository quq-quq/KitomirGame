using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    
    [SerializeField] private FadeScreen _fadeScreen;
    [SerializeField] private Spawner[] _spawnerList;
    
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == SceneNames.KITOMIR_HOME_SCENE)
        {
            GameStateManager.State = GameState.AtHome;
        }

        if (SceneManager.GetActiveScene().name == SceneNames.CORRIDOR_SCENE)
        {
            if (GameStateManager.PreviousSceneName == SceneNames.KITOMIR_HOME_SCENE)
            {
                return;
            }
            foreach (var spawner in _spawnerList)
            {
                if (spawner.Scene.name == GameStateManager.PreviousSceneName)
                {
                    Player.Instance.gameObject.transform.position = spawner.transform.position;
                }
            }
        }
        
        Door.OnDoorOpen += Door_OnDoorOpen;
        MenuButton.OnPlayButtonPressed += MenuButton_OnPlayButtonPressed;
        StartCoroutine(_fadeScreen.Appear(1.5f));
    }

    private void MenuButton_OnPlayButtonPressed(object sender, EventArgs e)
    {
        StartCoroutine(LoadScene(SceneNames.KITOMIR_HOME_SCENE));
    }

    private void Door_OnDoorOpen(object sender, Door.OnDoorOpenEventArgs e)
    {
        Player.Instance.CanAct = false;
        StartCoroutine(LoadScene(e.SceneToLoadName));
    }

    private IEnumerator LoadScene(string sceneName)
    {
        float waitAfterFadingDuration = 0f;
        
        // if (GameStateManager.State == GameState.AtHome)
        // {
        //     waitAfterFadingDuration = 13f;
        //     GameStateManager.State = GameState.PhysicsExam;
        // }
        // else if (GameStateManager.State == GameState.ITExam)
        // {
        //     waitAfterFadingDuration = 13f;
        //     GameStateManager.State = GameState.ExamsPassed;
        // }
        GameStateManager.PreviousSceneName = SceneManager.GetActiveScene().name;
        
        yield return StartCoroutine(_fadeScreen.Fade(1.5f, waitAfterFadingDuration));

        SceneManager.LoadScene(sceneName);
    }

    private void OnDisable()
    {
        Door.OnDoorOpen -= Door_OnDoorOpen;
    }
}
