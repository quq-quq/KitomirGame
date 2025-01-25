using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Dialogue_system
{
    public enum DialogueSide
    {
        Left,
        Right
    }
    public enum WriteType
    {
        Simple,
        Immediately,
        Robot,
        Robot01,
        Decoding,
        Decoding01,
        LastIsCapital
    }
    
    [System.Serializable]
    public class Dialogue
    {
        [SerializeField, Multiline(5)] private string _mainText;
        [SerializeField] private string _characterName;
        [SerializeField] private Sprite _avatar;
        [SerializeField] private Sprite _background;
        [SerializeField] private TMP_FontAsset _font;
        [SerializeField] private DialogueSide _side;
        [SerializeField] private float _speedOverride;
        [SerializeField] private WriteType _writeType;
        [SerializeField] private Color _colorText = Color.white;

        public Color ColorText
        {
            get => _colorText;
        }

        public WriteType WriteType
        {
            get => _writeType;
        }

        public float SpeedOverride
        {
            get => _speedOverride;
        }

        public string MainText
        {
            get => _mainText;
        }
        
        public string CharacterName
        {
            get => _characterName;
        }

        public Sprite Avatar
        {
            get => _avatar;
        }

        public Sprite Background
        {
            get => _background;
        }

        public TMP_FontAsset Font
        {
            get => _font;
        }

        public DialogueSide Side
        {
            get => _side;
        }
    }
}
