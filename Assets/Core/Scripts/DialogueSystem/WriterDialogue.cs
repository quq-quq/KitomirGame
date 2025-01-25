namespace Dialogue_system
{
    public abstract class WriterDialogue
    {
        protected string FinalString;
        
        public WriterDialogue(string finalString)
        {
            FinalString = finalString;
        }

        public abstract string EndText();

        public abstract string WriteNextStep();
    }
}
