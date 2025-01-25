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
        private struct TypeOfBunch
        {
            public Type dialogueBunch;
            public Type answersOfPlayerBunch;

            public TypeOfBunch()
            {
                dialogueBunch = typeof(DialogueBunch);
            }
        }

        [Header("Dialogue Viewer Settings")]
        [SerializeField] private Bunch _currentBunch;
        [SerializeField] private float _oneCharTime;
        [SerializeField] private bool _startOnStart;

        [Space]
        [Header("Links")]
        [SerializeField] private GameObject _parent;
        [SerializeField] private Image _avatar;
        [SerializeField] private Image _background;
        [SerializeField] private TMP_Text _mainText;
        [SerializeField] private TMP_Text _nameText;

        private int _currentDialogueBunch = 0;
        private int _currentIndexDialogue = 0;
        private bool _isWriting = false;
        private float _currentCharTime;
        private WriterDialogue _currentWriter;
        //private Dialogue CurrentDialogue => _startBunch.Dialogues[_currentIndexDialogue];
        //private int CountOfDialogue => _startBunch.Dialogues.Count;

        private void Start()
        {
            _parent.SetActive(false);
            _mainText.text = string.Empty;
            if (_startOnStart)
            {
                StartDialogue();
            }
        }

        private void Update()
        {
            if (!_isWriting) return;

            if (Input.GetKeyDown(KeyCode.Return))
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

            //it is an exit from Dialogue

            //if (Input.GetKeyDown(KeyCode.Escape))
            //{
            //    if (_mainText.text == _currentWriter.EndText())
            //    {
            //        EndDialogue();
            //    }
            //    else
            //    {
            //        _mainText.text = _currentWriter.EndText();
            //        StopCoroutine(WriteTextCoroutine());
            //        EndDialogue();
            //    }
            //}
        }

        public void StartDialogue()
        {
            if (_parent.activeSelf)
                return;
            _parent.SetActive(true);
            _currentCharTime = _oneCharTime;
            _currentIndexDialogue = 0;
            _mainText.text = string.Empty;
            ViewDialog(CurrentDialogue);
        }

        public void EndDialogue()
        {
            _currentIndexDialogue = 0;
            _mainText.text = string.Empty;
            _isWriting = false;
            _parent.SetActive(false);
        }

        public void NextDialogue()
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
