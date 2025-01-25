using UnityEngine;

namespace Dialogue_system
{
    public class RobotWriterDialogue : WriterDialogue
    {
        private string _currentClearText = "";
        private int _index = 0;
        private const string Alphobet = "@#%$^&*()!~|?></";
        private const int Depth = 5;

        private char RandomLiteral
        {
            get => Alphobet[Random.Range(0, Alphobet.Length)];
        }

        public RobotWriterDialogue(string finalString) : base(finalString)
        {
        }

        public override string EndText()
        {
            return FinalString;
        }

        public override string WriteNextStep()
        {
            if(FinalString.Length == _index + 1)
                return FinalString;
            _currentClearText += FinalString[_index];
            var text = _currentClearText;
            _index++;
            for (int i = 0; i < Depth && _index + i < FinalString.Length; i++)
            {
                text += RandomLiteral;
            }
            return text;
        }
    }
}
