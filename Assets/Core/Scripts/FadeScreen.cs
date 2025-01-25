using System;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour {
    
    public static FadeScreen Instance { get; private set; }

    public event EventHandler<OnFadeStartedEventArgs> OnFadeStarted;

    public class OnFadeStartedEventArgs : EventArgs {
        public float TotalTime;
    }
    
    public event EventHandler OnFadeComplete;
    public event EventHandler OnMotor;
    
    [SerializeField] private GameObject fadeImage;
    
    private float _fadeProgress;
    private float _timeInDarkness;
    private Color _color;
    private Image _image;
    private float _fadeDuration;
    private bool _hasMotorStarted = false;
    private int _factor = 1;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
         _image = fadeImage.GetComponent<Image>();
        _color = _image.color;
        Fade(1, 0, true);
    }

    private void Update() {
        if (_fadeProgress > 0f) {
            _fadeProgress -= Time.deltaTime;
            _color.a += (Time.deltaTime / _fadeDuration) * _factor;
            _image.color = _color;
        }

        if (_color.a >= 1f) {
            
            if (_timeInDarkness > 0f) {
                if (_hasMotorStarted == false) {
                    OnMotor?.Invoke(this, EventArgs.Empty);
                    _hasMotorStarted = true;
                }
                _timeInDarkness -= Time.deltaTime;
            }

            if ((_hasMotorStarted && _timeInDarkness <= 0f) || (!_hasMotorStarted)) {
                OnFadeComplete?.Invoke(this, EventArgs.Empty);
                _fadeProgress = 0f;
            }
        }
    }

    public void Fade(float duration, float timeInDarkness, bool isUnfaiding = false) {
        if (isUnfaiding) {
            _factor = -1;
            _color.a = 1f;
            _image.color = _color;
        }
        else {
            _factor = 1;
            _color.a = 0f;
            _image.color = _color;
        }
        fadeImage.SetActive(true);
        _fadeDuration = duration;
        _timeInDarkness = timeInDarkness;
        _fadeProgress = _fadeDuration;
        
        if (!isUnfaiding) {
            OnFadeStarted?.Invoke(this, new OnFadeStartedEventArgs {
                TotalTime = duration + timeInDarkness
            });
        }
    }
    
    
}