using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonContainer : MonoBehaviour
{
    [SerializeField] private GameObject _infoButton;
    
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == SceneInfo.MAIN_MENU_SCENE)
        {
            if (PlayerPrefs.HasKey("IsGameCompleted"))
            {
                if (PlayerPrefs.GetInt("IsGameCompleted") == 1)
                {
                    _infoButton.SetActive(true);
                    return;
                }
            }
        }
        _infoButton.SetActive(false);
    }
}
