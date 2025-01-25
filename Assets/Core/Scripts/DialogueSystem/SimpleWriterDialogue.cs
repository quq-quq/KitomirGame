namespace Dialogue_system
{
    public class SimpleWriterDialogue : WriterDialogue
    {

        private string _currentText = "";
        private int _index = 0;

        public SimpleWriterDialogue(string finalString) : base(finalString)
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
            _currentText += FinalString[_index];
            _index++;
            return _currentText;
        }
    }
}
