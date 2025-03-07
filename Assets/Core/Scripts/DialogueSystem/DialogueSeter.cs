using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueSeter
{
    private DialogueBunch _dialogueBunch;
    private List<DialogueBaseClass> _previousAnswers;

    public DialogueSeter( DialogueBunch dialogueBunch)
    {
        _previousAnswers = new List<DialogueBaseClass>();
        _dialogueBunch = dialogueBunch;
    }

    public DialogueBaseClass SetNewElementAtSimplePhrase(List<DialogueBaseClass> dialogue, DialogueBaseClass currentDialogueElement)
    {
        DialogueBaseClass nextDialogueElement = null;
        if (currentDialogueElement.TypeOfDialogue == TypeOfDialogue.SimplePhrases)
        {
            if (dialogue.Contains(currentDialogueElement))
            {
                if (dialogue[^1] == currentDialogueElement && _dialogueBunch.RootDialogue[^1] != currentDialogueElement)
                {
                    nextDialogueElement = SetNextElAfterPreviousAnswer(_dialogueBunch.RootDialogue, currentDialogueElement);
                    return nextDialogueElement;
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
            SetPreviousAnswers(_dialogueBunch.RootDialogue);

            _dialogueBunch.Reputation += addReputation;
            return nextDialogueElement;
        }
        return null;
    }

    private void SetPreviousAnswers(List<DialogueBaseClass> dialogue)
    {
        for(int i = 0; i < _previousAnswers.Count-1; i++)
        {
            if (dialogue.Contains(_previousAnswers[i]) && dialogue.Contains(_previousAnswers[^1]))
            {
                _previousAnswers.RemoveAt(i);
                return;
            }
        }

        foreach (DialogueBaseClass el in dialogue)
        {
            if (el.TypeOfDialogue == TypeOfDialogue.Answers)
            {
                foreach (DialogueBaseClass.Answer answer in el.Answers)
                {
                    SetPreviousAnswers(answer.NextDialogueBaseClasses);
                }
            }
        }
        return;
    }

    private DialogueBaseClass SetNextElAfterPreviousAnswer(List<DialogueBaseClass> dialogue, DialogueBaseClass currentDialogueElement)
    {
        if (currentDialogueElement.TypeOfDialogue == TypeOfDialogue.SimplePhrases)
        {
            DialogueBaseClass nextDialogueElement = null;
            if (dialogue.Contains(_previousAnswers[^1]))
            {
                if (dialogue[^1] != _previousAnswers[^1])
                {
                    nextDialogueElement = dialogue[dialogue.IndexOf(_previousAnswers[^1]) + 1];
                    return nextDialogueElement;
                }
            }
            foreach (DialogueBaseClass el in dialogue)
            {
                if (el.TypeOfDialogue == TypeOfDialogue.Answers)
                {
                    foreach (DialogueBaseClass.Answer answer in el.Answers)
                    {
                        nextDialogueElement = SetNextElAfterPreviousAnswer(answer.NextDialogueBaseClasses, currentDialogueElement);
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
}
