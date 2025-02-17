using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] private FadeScreen _fadeScreen;

    // Можно сделать этот объект don't destroy on load и хранить информацию о прошлой сцене здесь.
    // В таком случае у GameManager будет SerializeField поле - список спавнпоинтов на этой карте, каждый из которых
    // соответствует предыдущей сцене.
    // В таком случае придётся в иерархии вынести GameManager из Managers
    
    // Также можно хранить информацию о прошлой сцене в конфиге и загружать её оттуда
    
    private void Start()
    {
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
        if ((sceneName == SceneNames.CORRIDOR_SCENE && SceneManager.GetActiveScene().name == SceneNames.KITOMIR_HOME_SCENE) 
            || sceneName == SceneNames.HAPPY_END_SCENE)
        {
            waitAfterFadingDuration = 13f;
        }
        yield return StartCoroutine(_fadeScreen.Fade(1.5f, waitAfterFadingDuration));

        SceneManager.LoadScene(sceneName);
    }

    private void OnDisable()
    {
        Door.OnDoorOpen -= Door_OnDoorOpen;
    }
}
