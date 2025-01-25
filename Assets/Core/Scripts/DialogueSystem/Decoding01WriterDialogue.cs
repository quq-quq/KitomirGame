using System.Text;
using UnityEngine;

namespace Dialogue_system
{
    public class Decoding01WriterDialogue : WriterDialogue
    {
        private StringBuilder _currentText = new StringBuilder("");
        private int _index = 0;

        private char RandomLiteral
        {
            get => Random.Range(0, 2).ToString()[0];
        }
        
        public Decoding01WriterDialogue(string finalString) : base(finalString)
        {
            for (int i = 0; i < finalString.Length; i++)
            {
                if(char.IsWhiteSpace(finalString[i]))
                {
                    _currentText.Append(" ");
                }
                else
                {
                    _currentText.Append(RandomLiteral);
                }
            }
        }

        public override string EndText()
        {
            return FinalString;
        }

        public override string WriteNextStep()
        {
            if(FinalString.Length == _index + 1)
                return FinalString;
            _currentText[_index] = FinalString[_index];
            _index++;
            return _currentText.ToString();
        }
    }
}
