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
                //if (dialogue[dialogue.Count - 1] == currentDialogueElement && _dialogueBunch.RootDialogue[_dialogueBunch.RootDialogue.Count - 1] != currentDialogueElement)
                //{
                //    nextDialogueElement = SetNextElAfterPreviousAnswer(_dialogueBunch.RootDialogue);
                //    return nextDialogueElement;

                //}
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
            //_previousAnswers.Add(nextDialogueElement);
            //SetPreviousAnswers(_dialogueBunch.RootDialogue);

            _dialogueBunch.Reputation += addReputation;
            return nextDialogueElement;
        }
        return null;
    }

    //private void SetPreviousAnswers(List<DialogueBaseClass> dialogue)
    //{
    //    if (_currentDialogueElement.TypeOfDialogue == TypeOfDialogue.Answers)
    //    {
    //        if (dialogue.Contains(_previousAnswers[_previousAnswers.Count - 2]) && dialogue.Contains(_previousAnswers[_previousAnswers.Count - 1]))
    //        {
    //            _previousAnswers.RemoveAt(_previousAnswers.Count - 2);
    //            return;
    //        }
    //        foreach (DialogueBaseClass el in dialogue)
    //        {
    //            if (el.TypeOfDialogue == TypeOfDialogue.Answers)
    //            {
    //                foreach (DialogueBaseClass.Answer answer in el.Answers)
    //                {
    //                    SetPreviousAnswers(answer.NextDialogueBaseClasses);
    //                }
    //            }
    //        }
    //    }
    //    return;
    //}

    //private DialogueBaseClass SetNextElAfterPreviousAnswer(List<DialogueBaseClass> dialogue)
    //{
    //    if (_currentDialogueElement.TypeOfDialogue == TypeOfDialogue.SimplePhrases)
    //    {
    //        DialogueBaseClass currentDialogueElement = null;
    //        if (dialogue.Contains(_previousAnswers[_previousAnswers.Count - 1]))
    //        {
    //            if (dialogue[dialogue.Count - 1] != _previousAnswers[_previousAnswers.Count - 1])
    //            {
    //                currentDialogueElement = dialogue[dialogue.IndexOf(_previousAnswers[_previousAnswers.Count - 1]) + 1];
    //                return currentDialogueElement;
    //            }
    //        }
    //        foreach (DialogueBaseClass el in dialogue)
    //        {
    //            if (el.TypeOfDialogue == TypeOfDialogue.Answers)
    //            {
    //                foreach (DialogueBaseClass.Answer answer in el.Answers)
    //                {
    //                    currentDialogueElement = SetNextElAfterPreviousAnswer(answer.NextDialogueBaseClasses);
    //                    if (currentDialogueElement != null)
    //                    {
    //                        return currentDialogueElement;
    //                    }
    //                }
    //            }
    //        }

    //    }
    //    return null;
    //}
}
