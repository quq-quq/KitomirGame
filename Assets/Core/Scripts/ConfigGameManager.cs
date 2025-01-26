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

    Dictionary<string, Vector3> _doorPositions = new Dictionary<string, Vector3>();
    
    private Transform _lastEntranceTransform;
    public Vector3 spawnAtOnNewScene;
    
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(this.gameObject);
        
        spawnAtOnNewScene = Vector3.zero;
    }
    //
    // private void Start() {
    //     _doorPositions["MathsAuditoriumScene"] = new Vector3(-15, -0.16f,0);
    //     _doorPositions["ITAuditoriumScene"] = new Vector3(3, -0.16f, 0);
    //     _doorPositions["PhysicsAuditoriumScene"] = new Vector3(48, -0.16f, 0);
    //     _doorPositions["KitomirHomeScene"] = new Vector3(-47, -0.16f, 0);  
    // }

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

    // public void SetPlayerPosition(string sceneName) {
    //     if (_doorPositions.ContainsKey(sceneName)) {
    //         spawnAtOnNewScene = _doorPositions[sceneName];
    //     }
    // }
    
    private void StartHappyEnd() {}
    
    private void StartSadEnd() {}
}
