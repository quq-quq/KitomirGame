using System.Text;

namespace Dialogue_system
{
    public class LastIsCapitalWriterDialogue : WriterDialogue
    {
        private StringBuilder _currentText = new StringBuilder("");
        private int _index = 0;

        public LastIsCapitalWriterDialogue(string finalString) : base(finalString)
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
            _currentText.Append(char.ToUpper(FinalString[_index]));
            if(_index != 0)
                _currentText[_index - 1] = FinalString[_index - 1];
            _index++;
            return _currentText.ToString();
        }
    }
}
