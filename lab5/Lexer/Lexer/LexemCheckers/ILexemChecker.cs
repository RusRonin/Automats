namespace Lexer.LexemCheckers
{
    public interface ILexemChecker
    {
        public LexemType LexemType { get; }

        public bool Check(string input);
    }
}
