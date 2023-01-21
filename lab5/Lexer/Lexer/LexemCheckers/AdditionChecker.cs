namespace Lexer.LexemCheckers
{
    public class AdditionChecker : ILexemChecker
    {
        public LexemType LexemType => LexemType.Addition;

        public bool Check(string input)
        {
            return input.Equals("+");
        }
    }
}
