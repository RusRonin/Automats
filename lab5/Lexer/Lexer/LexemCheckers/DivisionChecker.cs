namespace Lexer.LexemCheckers
{
    public class DivisionChecker : ILexemChecker
    {
        public LexemType LexemType => LexemType.Division;

        public bool Check(string input)
        {
            return input.Equals("/");
        }
    }
}
