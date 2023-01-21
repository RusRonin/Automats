namespace Lexer.LexemCheckers
{
    public class IdentifierChecker : ILexemChecker
    {
        public LexemType LexemType => LexemType.Identifier;

        public bool Check(string input)
        {
            bool valid = (input[0] == '_') || char.IsLetter(input[0]);
            for (int i = 1; i < input.Length; i++)
            {
                valid = valid && (input[i] == '_' || char.IsLetterOrDigit(input[i]));
            }
            return valid;
        }
    }
}
