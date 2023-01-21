namespace Lexer.LexemCheckers
{
    public class MultiplicationChecker : ILexemChecker
    {
        public LexemType LexemType => LexemType.Multiplication;

        public bool Check(string input)
        {
            return input.Equals("*");
        }
    }
}
