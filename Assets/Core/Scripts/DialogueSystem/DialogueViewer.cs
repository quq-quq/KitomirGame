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
        [SerializeField] bool _isItADialog;
        [SerializeField] DialogueBunch _startBunch;
        
        [SerializeField] private float _oneCharTime;

        [Space]
        [Header("Links")]
        [SerializeField] private GameObject _parent;
        [SerializeField] private Image _avatar;
        [SerializeField] private Image _background;
        [SerializeField] private TMP_Text _mainText;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] Button _buttonForAnswersOfPlayer;
        [Space(20)]
        [SerializeField] private List<DialogueViewer> _Answers;
        [SerializeField] private List<DialogueViewer> _dialogueViewers;

        private int _currentIndexDialogue = 0;
        private bool _isWriting = false;
        private float _currentCharTime;
        private WriterDialogue _currentWriter;
        private DialogueBunch _bunch;
        private Dialogue CurrentDialogue => _bunch.Dialogues[_currentIndexDialogue];
        private int CountOfDialogue => _bunch.Dialogues.Count;

        private void OnEnable()
        {
            BunchBus.StartOrContinueDialogue += StartDialogue;
            _buttonForAnswersOfPlayer.onClick.AddListener(EndDialogue);

            if (_startBunch != null)
            {
                _bunch = _startBunch;
                StartDialogue(_bunch);
            }
        }

        private void OnDisable()
        {
            BunchBus.StartOrContinueDialogue -= StartDialogue;
            _buttonForAnswersOfPlayer.onClick.RemoveListener(EndDialogue);
        }

        private void Awake()
        {
            _parent.SetActive(false);
            _mainText.text = string.Empty;
        }


        private void Update()
        {
            if (!_isWriting) return;

            if (Input.GetKeyDown(KeyCode.Return) && _isItADialog)
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
            _bunch = dialogueBunch;
            if (_parent.activeSelf)
                return;
            _parent.SetActive(true);
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
            _parent.SetActive(false);



            //if(_bunch.NextBunch != null)
            //{
            //    if (_bunch.NextBunch is DialogueBunch nextDialogueBunch)
            //    {
            //        BunchBus.StartOrContinueDialogue?.Invoke(nextDialogueBunch);
            //    }

            //    if (_bunch.NextBunch is AnswersOfPlayerBunch answersOfPlayerBunch)
            //    {
            //        BunchBus.StartOrContinueAnswersOfPlayer?.Invoke(answersOfPlayerBunch);
            //    }
            //}

            if (_isItADialog)
            {
                _Answers[0].gameObject.SetActive(true);
                _Answers[0].gameObject.SetActive(true);
                _Answers[0].gameObject.SetActive(true);
                _dialogueViewers[0].gameObject.SetActive(false);
                _dialogueViewers.RemoveAt(0);
            }
            else
            {
                _dialogueViewers[0].gameObject.SetActive(true);
                _Answers[0].gameObject.SetActive(false);
                _Answers[0].gameObject.SetActive(false);
                _Answers[0].gameObject.SetActive(false);
                _Answers.RemoveAt(0);
                _Answers.RemoveAt(0);
                _Answers.RemoveAt(0);
            }
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
            _mainText.color = dialogue.ColorText;
            _nameText.text = dialogue.CharacterName;
            _currentCharTime = dialogue.SpeedOverride > 0 ? dialogue.SpeedOverride : _oneCharTime;
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
