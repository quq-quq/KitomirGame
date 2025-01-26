using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfigGameManager : MonoBehaviour {

    public static ConfigGameManager Instance { get; private set; }

    [SerializeField] private bool _isPhysicsPassed = false;
    [SerializeField] private bool _isMathsPassed = false;
    [SerializeField] private bool _isITPassed = false;
    [SerializeField] private bool _isAnyExamFailed = false;
    [SerializeField] private bool _isGameCompleted = false;
    
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(this.gameObject);
    }

    public bool IsDoorInteractable(string sceneName) {
        if (sceneName == "MathsAuditoriumScene" && !_isPhysicsPassed) {
            return false;
        }

        if (sceneName == "ITAuditoriumScene" && !_isMathsPassed) {
            return false;
        }

        if (sceneName == "KitomirHappyFinal" && !_isITPassed) {
            return false;
        }

        return true;
    }
}
