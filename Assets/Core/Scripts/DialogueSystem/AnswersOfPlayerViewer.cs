using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Dialogue_system
{

    public class AnswersOfPlayerViewer : MonoBehaviour
    {
        [Header("Answers Of Player Viewer Settings")]

        [SerializeField] private float _oneCharTime;
        [SerializeField] string _name;
        //[SerializeField] private bool _startDialogueOnStart;

        [Space]
        [Header("Links")]
        [SerializeField] private GameObject _parent;
        [SerializeField] private Image _avatar;
        [SerializeField] private Image _background;
        [SerializeField] private TMP_Text _mainText;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_FontAsset _font;
        [SerializeField] private Color _colorOfText;

        private bool _isWriting = false;
        private float _currentCharTime;
        private WriterDialogue _currentWriter;
        private AnswersOfPlayerBunch _bunch;

        private int _answersCount => _bunch.AnswerAndNextBunches.Count;

        private void OnEnable()
        {
            BunchBus.StartOrContinueAnswersOfPlayer += StartDialogueAnswersOfPlayer;
            _button.onClick.AddListener(EndDialogue);
        }

        private void OnDisable()
        {
            BunchBus.StartOrContinueAnswersOfPlayer -= StartDialogueAnswersOfPlayer;
            _button.onClick.RemoveListener(EndDialogue);
        }

        private void Start()
        {
            _mainText.text = string.Empty;
            _parent.SetActive(false);
            //if (_startDialogueOnStart)
            //{
            //    StartDialogue();
            //}
        }

        public void StartDialogueAnswersOfPlayer(AnswersOfPlayerBunch answersOfPlayerBunch)
        {
            _bunch = answersOfPlayerBunch;
            if (_parent.activeSelf)
                return;
            _parent.SetActive(true);
            _currentCharTime = _oneCharTime;
            _mainText.text = string.Empty;
            ViewDialog();
        }

        private void EndDialogue()
        {
            StopCoroutine(WriteTextCoroutine());
            _mainText.text = string.Empty;
            _isWriting = false;
            _parent.SetActive(false);

            if (_bunch.NextBunch != null)
            {
                if (_bunch.NextBunch is DialogueBunch nextDialogueBunch)
                {
                    BunchBus.StartOrContinueDialogue?.Invoke(nextDialogueBunch);
                }

                if (_bunch.NextBunch is AnswersOfPlayerBunch answersOfPlayerBunch)
                {
                    BunchBus.StartOrContinueAnswersOfPlayer?.Invoke(answersOfPlayerBunch);
                }
            }
        }

        private void ViewDialog()
        {
            _currentWriter = WriterDialogueFabric.GetWriterOfType(WriteType.Simple, _bunch.AnswerAndNextBunches[_answersCount].answerText);
            if (_avatar != null)
            {
                _avatar.color = Color.white;
            }
            else
            {
                _avatar.color = Color.clear;
            }
            _mainText.font = _font;
            _mainText.color = _colorOfText;
            _nameText.text = _name;
            _currentCharTime = _oneCharTime;
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
