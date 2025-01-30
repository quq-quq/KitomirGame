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
        LoadScene(e.SceneToLoadName);
    }

    private void LoadScene(string sceneName)
    {
        FadeScreen fadeScreen = Instantiate(_fadeScreenPrefab).GetComponent<FadeScreen>();
        float waitAfterFadingDuration = 0f;
        if (sceneName is SceneNames.KITOMIR_HOME_SCENE or SceneNames.HAPPY_END_SCENE)
        {
            // todo vehicle motor sound
            waitAfterFadingDuration = 13f;
        }
        StartCoroutine(fadeScreen.Fade(1.5f, waitAfterFadingDuration));
        
        // todo load scene (the problem is that the LoadScene method does not wait for the end of the Fade coroutine)
    }
}
