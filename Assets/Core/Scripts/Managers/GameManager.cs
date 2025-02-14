using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FadeScreen _fadeScreen;

    private void Start()
    {
        Door.OnDoorOpen += Door_OnDoorOpen;
        StartCoroutine(_fadeScreen.Appear(1.5f));
    }

    private void Door_OnDoorOpen(object sender, Door.OnDoorOpenEventArgs e)
    {
        Player.Instance.CanAct = false;
        StartCoroutine(LoadScene(e.SceneToLoadName));
    }

    private IEnumerator LoadScene(string sceneName)
    {
        float waitAfterFadingDuration = 0f;
        if (sceneName is SceneNames.KITOMIR_HOME_SCENE or SceneNames.HAPPY_END_SCENE)
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
