using System;
using System.Collections.Generic;

namespace Lexer.Extensions
{
    public static class LexemTypeExtensions
    {
        public static TokenType ToTokenType(this LexemType lexemType)
        {
            Dictionary<LexemType, TokenType> map = new Dictionary<LexemType, TokenType>
            {
                { LexemType.Keyword, TokenType.Keyword },
                { LexemType.Identifier, TokenType.Identifier },
                { LexemType.Assignment, TokenType.Assigment },
                { LexemType.Comparation, TokenType.Comparator },
                { LexemType.Number, TokenType.Number },
                { LexemType.Separator, TokenType.Separator },
                { LexemType.String, TokenType.String },
                { LexemType.Addition, TokenType.Addition },
                { LexemType.Substraction, TokenType.Substraction },
                { LexemType.Multiplication, TokenType.Multiplication },
                { LexemType.Division, TokenType.Division }
            };

            if (!map.ContainsKey(lexemType))
            {
                throw new ApplicationException("Unknown lexem");
            }
            return map[lexemType];
        }
    }
}
