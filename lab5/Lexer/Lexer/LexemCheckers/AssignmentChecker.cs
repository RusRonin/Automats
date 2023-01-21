namespace Lexer.LexemCheckers
{
    public class AssignmentChecker : ILexemChecker
    {
        public LexemType LexemType => LexemType.Assignment;

        public bool Check(string input)
        {
            return input.Equals("=");
        }
    }
}
