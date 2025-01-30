using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Door[] _doorsOnScene;
    [SerializeField] private GameObject _fadeScreenPrefab;

    private void Start()
    {
        foreach (Door door in _doorsOnScene)
        {
            door.OnDoorOpen += Door_OnDoorOpen;
        }
    }

    private void Door_OnDoorOpen(object sender, Door.OnDoorOpenEventArgs e)
    {
        // todo sound of door opening
        StartCoroutine(LoadScene(e.SceneToLoadName));
    }

    private IEnumerator LoadScene(string sceneName)
    {
        FadeScreen fadeScreen = Instantiate(_fadeScreenPrefab).GetComponent<FadeScreen>();
        float waitAfterFadingDuration = 0f;
        if (sceneName is SceneNames.KITOMIR_HOME_SCENE or SceneNames.HAPPY_END_SCENE)
        {
            // todo vehicle motor sound
            waitAfterFadingDuration = 13f;
        }
        yield return StartCoroutine(fadeScreen.Fade(1.5f, waitAfterFadingDuration));
        
        Debug.Log("" + sceneName + "is ready to get loaded");
    }
}
