using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfigGameManager : MonoBehaviour {

    public static ConfigGameManager Instance { get; private set; }

    [SerializeField] private bool _isPhysicsPassed = true;
    [SerializeField] private bool _isMathsPassed = true;
    [SerializeField] private bool _isITPassed = false;
    [SerializeField] private bool _isAnyExamFailed = false;
    [SerializeField] private bool _isGameCompleted = false;
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
    
    public void ExamPassed(string sceneName) {
        if (sceneName == "PhysicsAuditoriumScene") {
            _isPhysicsPassed = true;
        }    
        if (sceneName == "MathsAuditoriumScene") {
            _isMathsPassed = true;
        }    
        if (sceneName == "ITAuditoriumScene") {
            _isITPassed = true;
            _isGameCompleted = true;
            StartHappyEnd();
        }    
    }

    public void ExamFailed() {
        _isAnyExamFailed = true;
        StartSadEnd();
    }
    
    private void StartHappyEnd() {}
    
    private void StartSadEnd() {}
}
