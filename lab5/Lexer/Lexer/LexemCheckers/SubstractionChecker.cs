namespace Lexer.LexemCheckers
{
    public class SubstractionChecker : ILexemChecker
    {
        public LexemType LexemType => LexemType.Substraction;

        public bool Check(string input)
        {
            return input.Equals("-");
        }
    }
}
