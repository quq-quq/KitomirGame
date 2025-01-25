using System;

namespace Dialogue_system
{
    public static class WriterDialogueFabric
    {
        public static WriterDialogue GetWriterOfType(WriteType type, string final)
        {
            return type switch
            {
                WriteType.Simple => new SimpleWriterDialogue(final),
                WriteType.Immediately => new ImmediatelyWriterDialogue(final),
                WriteType.Robot => new RobotWriterDialogue(final),
                WriteType.Robot01 => new Robot01WriterDialogue(final),
                WriteType.Decoding => new DecodingWriterDialogue(final),
                WriteType.Decoding01 => new Decoding01WriterDialogue(final),
                WriteType.LastIsCapital => new LastIsCapitalWriterDialogue(final),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}
