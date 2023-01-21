namespace Lexer.LexemCheckers
{
    public class CommentChecker : ILexemChecker
    {
        public LexemType LexemType => LexemType.Comment;

        public bool Check(string input)
        {
            return input.StartsWith("//");
        }
    }
}
