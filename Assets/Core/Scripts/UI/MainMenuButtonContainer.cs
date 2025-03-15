using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonContainer : MonoBehaviour
{
    [SerializeField] private ButtonContainer _buttonContainer;
    [SerializeField] private GameObject _infoButton;
    
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == SceneInfo.MAIN_MENU_SCENE)
        {
            if (PlayerPrefs.HasKey("IsGameCompleted"))
            {
                if (PlayerPrefs.GetInt("IsGameCompleted") == 1)
                {
                    _buttonContainer.SetButtonActive(_infoButton,true);
                    return;
                }
            }
        }
        _buttonContainer.SetButtonActive(_infoButton, false);
    }
}
