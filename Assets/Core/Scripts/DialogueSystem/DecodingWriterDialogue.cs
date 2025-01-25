using System.Text;
using UnityEngine;

namespace Dialogue_system
{
    public class DecodingWriterDialogue : WriterDialogue
    {
        private StringBuilder _currentText = new StringBuilder("");
        private int _index = 0;
        
        private const string Alphobet = "@#%$^&*()!~|?></";
        
        private char RandomLiteral
        {
            get => Alphobet[Random.Range(0, Alphobet.Length)];
        }
        
        public DecodingWriterDialogue(string finalString) : base(finalString)
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
