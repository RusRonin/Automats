using System.Collections.Generic;

namespace Lexer.LexemCheckers
{
    public class SeparatorChecker : ILexemChecker
    {
        private static List<string> Separators = new List<string>
        {
            ";", "[", "]", "(", ")", "{", "}", ":"
        };

        public LexemType LexemType => LexemType.Separator;

        public bool Check(string input)
        {
            return Separators.Contains(input);
        }
    }
}
