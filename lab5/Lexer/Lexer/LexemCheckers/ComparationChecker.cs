using System.Collections.Generic;

namespace Lexer.LexemCheckers
{
    public class ComparationChecker : ILexemChecker
    {
        private static List<string> _comparators = new List<string>
        {
            "==", "<", ">", "<=", ">=", "!="
        };

        public LexemType LexemType => LexemType.Comparation;

        public bool Check(string input)
        {
            return _comparators.Contains(input);
        }
    }
}
