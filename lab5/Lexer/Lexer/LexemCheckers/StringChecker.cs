using System.Linq;

namespace Lexer.LexemCheckers
{
    public class StringChecker : ILexemChecker
    {
        public LexemType LexemType => LexemType.String;

        public bool Check(string input)
        {
            return input.First() == '"' && input.Last() == '"';
        }
    }
}
