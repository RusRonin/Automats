using System.Collections.Generic;

namespace Lexer.LexemCheckers
{
    public class KeywordChecker : ILexemChecker
    {
        public LexemType LexemType => LexemType.Keyword;

        private static List<string> Keywords = new List<string>
        {
            "int", "float", "bool", "string", "var", "const", "while", "for", "if", "else", "switch", "case", 
            "false", "true", "return", "break", "continue", "default"
        };

        public bool Check(string input)
        {
            return Keywords.Contains(input);
        }
    }
}
