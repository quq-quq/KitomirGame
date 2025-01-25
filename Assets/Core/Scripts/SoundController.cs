using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SoundController : MonoBehaviour
{
    public static SoundController sounder { get; private set; }

    [SerializeField] private int _sourcesCount;

    private struct AudioData
    {
        public AudioSource audioSource;
        public string objectName;
    }

    private AudioData[] _audioDataArray;

    private void Awake()
    {
        if (sounder == null)
        {
            sounder = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }

        if (gameObject.GetComponents<AudioSource>().Length != _sourcesCount)
        {
            for (int i = 0; i < _sourcesCount; i++)
            {
                gameObject.AddComponent<AudioSource>();
            }
        }

        _audioDataArray = new AudioData[_sourcesCount];
        for (int i = 0; i < _sourcesCount; i++)
        {
            _audioDataArray[i].audioSource = gameObject.GetComponents<AudioSource>()[i];
            _audioDataArray[i].audioSource.clip = null;
            _audioDataArray[i].audioSource.loop = false;
            _audioDataArray[i].audioSource.volume = 1;
        }
    }

    public void SetSound(AudioClip clip, bool isLooped, string objectName, float volume)
    {
        void Seter(int index)
        {
            _audioDataArray[index].audioSource.clip = clip;
            _audioDataArray[index].audioSource.loop = isLooped;
            _audioDataArray[index].audioSource.volume = volume;
            _audioDataArray[index].objectName = objectName;
            _audioDataArray[index].audioSource.Play();
        }

        if (volume > 1)
        {
            volume = 1;
        }
        else if (volume < 0)
        {
            volume = 0;
        }


        for (int i = 0; i < _sourcesCount; i++)
        {
            if (objectName == _audioDataArray[i].objectName && _audioDataArray[i].audioSource.clip == clip && _audioDataArray[i].audioSource.isPlaying)
            {
                return;
            }
        }

        for (int i = 0; i < _sourcesCount; i++)
        {
            if (objectName == _audioDataArray[i].objectName || _audioDataArray[i].objectName == null)
            {
                Seter(i);
                return;
            }
        }

        for (int i = 0; i < _sourcesCount; i++)
        {
            if (!_audioDataArray[i].audioSource.isPlaying && !_audioDataArray[i].audioSource.loop)
            {
                Seter(i);
                return;
            }
        }
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        for (int i = 0; i < _sourcesCount; i++)
        {
            _audioDataArray[i].audioSource.clip = null;
            _audioDataArray[i].audioSource.loop = false;
            
        }
    }
}