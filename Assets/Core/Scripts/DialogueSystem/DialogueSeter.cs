using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueSeter
{
    private DialogueBunch _dialogueBunch;
    private List<DialogueBaseClass> _previousAnswers;
    private List<DialogueBaseClass> _nextSimplePhrases;

    public DialogueSeter( DialogueBunch dialogueBunch)
    {
        _previousAnswers = new List<DialogueBaseClass>();
        _nextSimplePhrases = new List<DialogueBaseClass>();
        _dialogueBunch = dialogueBunch;
    }

    private DialogueBaseClass SetNewDialogue()
    {
        if(_dialogueBunch.Reputation < _dialogueBunch.MinReputation)
        {
            _dialogueBunch.RootDialogue = _dialogueBunch.BadResultDialogue;
            return _dialogueBunch.RootDialogue[0];
        }
        if(_dialogueBunch.Reputation > _dialogueBunch.MaxReputation)
        {
            _dialogueBunch.RootDialogue = _dialogueBunch.GoodResultDialogue;
            return _dialogueBunch.RootDialogue[0];
        }
        return null;
    }

    public DialogueBaseClass SetNewElementAtSimplePhrase(List<DialogueBaseClass> dialogue, DialogueBaseClass currentDialogueElement)
    {
        DialogueBaseClass nextDialogueElement = null;
        if (currentDialogueElement.TypeOfDialogue == TypeOfDialogue.SimplePhrases)
        {
            if (dialogue.Contains(currentDialogueElement))
            {
                if (dialogue[^1] == currentDialogueElement)
                {
                    if (_nextSimplePhrases.Count != 0)
                    {
                        nextDialogueElement = _nextSimplePhrases[^1];
                        _previousAnswers.RemoveAt(_previousAnswers.Count - 1);
                        _nextSimplePhrases.RemoveAt(_nextSimplePhrases.Count - 1);
                        return nextDialogueElement;
                    }
                    else
                    {
                        return null;
                    }
                }

                nextDialogueElement = dialogue[dialogue.IndexOf(currentDialogueElement) + 1];
                return nextDialogueElement;
            }
            for (int i = 0; i < dialogue.Count; i++)
            {
                if (dialogue[i].TypeOfDialogue == TypeOfDialogue.Answers)
                {
                    foreach (DialogueBaseClass.Answer answer in dialogue[i].Answers)
                    {
                        nextDialogueElement = SetNewElementAtSimplePhrase(answer.NextDialogueBaseClasses, currentDialogueElement);
                        if (nextDialogueElement != null)
                        {
                            return nextDialogueElement;
                        }
                    }
                }
            }
        }
        return null;
    }

    public DialogueBaseClass SetNewElementAtAnswer(DialogueBaseClass nextDialogueElement, float addReputation, DialogueBaseClass currentDialogueElement)
    {
        if (currentDialogueElement.TypeOfDialogue == TypeOfDialogue.Answers)
        {
            _previousAnswers.Add(currentDialogueElement);
            SetLastAnswerInList(_dialogueBunch.RootDialogue);
            SetPreviousAnswersInList(_dialogueBunch.RootDialogue);

            _dialogueBunch.Reputation += addReputation;
            return nextDialogueElement;
        }
        return null;
    }

    private void SetLastAnswerInList(List<DialogueBaseClass> dialogue)
    {
        if (dialogue.Contains(_previousAnswers[^1]))
        {
            if(dialogue.IndexOf(_previousAnswers[^1]) == dialogue.Count - 1)
            {
                _previousAnswers.RemoveAt(_previousAnswers.Count - 1);
                return;
            }
            _nextSimplePhrases.Add(dialogue[dialogue.IndexOf(_previousAnswers[^1]) + 1]);
        }

        foreach (DialogueBaseClass el in dialogue)
        {
            if (el.TypeOfDialogue == TypeOfDialogue.Answers)
            {
                foreach (DialogueBaseClass.Answer answer in el.Answers)
                {
                    SetPreviousAnswersInList(answer.NextDialogueBaseClasses);
                }
            }
        }
    }

    private void SetPreviousAnswersInList(List<DialogueBaseClass> dialogue)
    {
        for(int i = 0; i < _previousAnswers.Count-1; i++)
        {
            if (dialogue.Contains(_previousAnswers[i]) && dialogue.Contains(_previousAnswers[^1]))
            {
                _previousAnswers.RemoveAt(i);
                _nextSimplePhrases.RemoveAt(i);
                return;
            }
        }

        foreach (DialogueBaseClass el in dialogue)
        {
            if (el.TypeOfDialogue == TypeOfDialogue.Answers)
            {
                foreach (DialogueBaseClass.Answer answer in el.Answers)
                {
                    SetPreviousAnswersInList(answer.NextDialogueBaseClasses);
                }
            }
        }
        return;
    }
}
