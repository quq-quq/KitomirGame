using Dialogue_system;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Answer Of Player Bunch", menuName = "Answer Of Player Bunch", order = 0)]
public class AnswersOfPlayerBunch : Bunch
{
    [System.Serializable]
    public struct AnswerAndNextBunch
    {
        public string answerText;
        public Bunch NextBunch;
    }

    [SerializeField] private List<AnswerAndNextBunch> _answerAndNextBunches;

    public IReadOnlyList<AnswerAndNextBunch> AnswerAndNextBunches
    {
        get => _answerAndNextBunches;
    }
}
