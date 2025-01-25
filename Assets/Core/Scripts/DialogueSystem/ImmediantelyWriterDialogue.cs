namespace Dialogue_system
{
    public class ImmediatelyWriterDialogue : WriterDialogue
    {

        public ImmediatelyWriterDialogue(string finalString) : base(finalString)
        {
        }

        public override string EndText()
        {
            return FinalString;
        }

        public override string WriteNextStep()
        {
            return FinalString;
        }
    }
}
