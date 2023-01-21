using System.Collections.Generic;

namespace Lexer.Extensions
{
    public static class TokenTypeExtensions
    {
        public static string ToStringValue(this TokenType type)
        {
            Dictionary<TokenType, string> stringTypes = new Dictionary<TokenType, string>
            {
                { TokenType.Identifier, "Identifier" },
                { TokenType.Keyword, "Keyword" },
                { TokenType.Number, "Number" },
                { TokenType.Addition, "Addition" },
                { TokenType.Substraction, "Substraction" },
                { TokenType.Multiplication, "Multiplication" },
                { TokenType.Division, "Division" },
                { TokenType.Assigment, "Assigment" },
                { TokenType.Comparator, "Comparator" },
                { TokenType.Separator, "Separator" },
                { TokenType.String, "String" }
            };
            return stringTypes[type];
        }
    }
}
