using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue_system
{
    public class DialogueViewer : MonoBehaviour
    {

        [Header("Dialogue Viewer Settings")]
        [SerializeField] bool _StartDialogueOnStart;
        [SerializeField] bool _isItADialog;
        [SerializeField] private DialogueBunch _bunch;
        

        
        [SerializeField] private float _oneCharTime;

        [Space]
        [Header("Links")]
        [SerializeField] private GameObject _parent;//for destroying
        [SerializeField] private Image _avatar;
        [SerializeField] private Image _background;
        [SerializeField] private TMP_Text _mainText;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] Button _buttonForAnswersOfPlayer;
        [SerializeField] ReputationChange _reputationScrollBar;
        [Space(20)]
        [SerializeField] private List<DialogueViewer> _nextBunches;

        private int _currentIndexDialogue = 0;
        private bool _isWriting = false;
        private bool _isStarting = false;
        private float _currentCharTime;
        private WriterDialogue _currentWriter;
        private Dialogue CurrentDialogue => _bunch.Dialogues[_currentIndexDialogue];
        private int CountOfDialogue => _bunch.Dialogues.Count;

        private void OnEnable()
        {
            if(!_isItADialog)
                _buttonForAnswersOfPlayer.onClick.AddListener(EndDialogue);

            if (_bunch != null)
            {
                StartDialogue(_bunch);
            }
            else
            {
                Destroy(_parent);
            }
        }

        private void OnDisable()
        {
            if(!_isItADialog)
                _buttonForAnswersOfPlayer.onClick.RemoveListener(EndDialogue);
        }

        private void Awake()
        {
            if (!_StartDialogueOnStart)
            {
                _mainText.text = string.Empty;
                _parent.SetActive(false);
                
            }

        }


        private void Update()
        {
            if (!_isWriting) return;

            if (Input.GetMouseButtonDown(0) && _isItADialog)
            {
                if (_mainText.text == _currentWriter.EndText())
                {
                    NextDialogue();
                }
                else
                {
                    _mainText.text = _currentWriter.EndText();
                    StopCoroutine(WriteTextCoroutine());
                }
            }
        }

        public void StartDialogue(DialogueBunch dialogueBunch)
        {
            if (_isStarting)
                return;
            _isStarting = true;
            _currentCharTime = _oneCharTime;
            _currentIndexDialogue = 0;
            _mainText.text = string.Empty;
            ViewDialog(CurrentDialogue);
        }

        private void EndDialogue()
        {
            StopCoroutine(WriteTextCoroutine());
            _currentIndexDialogue = 0;
            _mainText.text = string.Empty;
            _isWriting = false;
            _isStarting= false;

            if (_nextBunches.Count > 0)
            {
                if (_isItADialog)
                {
                    for (int i = 0; i < _nextBunches.Count; i++)
                    {
                        _nextBunches[i]._parent.SetActive(true);
                    }
                }
                else
                {
                    _nextBunches[0]._parent.SetActive(true);
                    Debug.Log("hi");
                }
            }
            Destroy(_parent);
        }

        private void NextDialogue()
        {
            StopCoroutine(WriteTextCoroutine());
            if (_currentIndexDialogue + 1 == CountOfDialogue)
            {
                EndDialogue();
            }
            else
            {
                _currentIndexDialogue++;
                _mainText.text = string.Empty;
                ViewDialog(CurrentDialogue);
            }
        }

        private void ViewDialog(Dialogue dialogue)
        {
            _currentWriter = WriterDialogueFabric.GetWriterOfType(dialogue.WriteType, dialogue.MainText);
            _background.sprite = dialogue.Background;
            if (dialogue.Avatar != null)
            {
                _avatar.color = Color.white;
                _avatar.sprite = dialogue.Avatar;
            }
            else
            {
                _avatar.color = Color.clear;
            }
            _mainText.font = dialogue.Font;
            _mainText.color = Color.black;
            _nameText.color = Color.black;
            _nameText.text = dialogue.CharacterName;
            _currentCharTime = dialogue.SpeedOverride > 0 ? dialogue.SpeedOverride : _oneCharTime;
            _mainText.text = dialogue.MainText;
            _reputationScrollBar.ChangeReputation(dialogue.ReputationOfSpeech);
            StartCoroutine(WriteTextCoroutine());
        }

        private IEnumerator WriteTextCoroutine()
        {
            _isWriting = true;
            while (_mainText.text != _currentWriter.EndText())
            {
                _mainText.text = _currentWriter.WriteNextStep();
                yield return new WaitForSeconds(_currentCharTime);
            }
        }
    }
}
