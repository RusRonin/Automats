using System.Collections.Generic;
using System.Linq;

namespace Lexer.LexemCheckers
{
    public class NumberChecker : ILexemChecker
    {
        public LexemType LexemType => LexemType.Number;

        public bool Check(string input)
        {
            return CheckInteger(input) || CheckFloat(input) || CheckBinary(input) || CheckOctal(input) || CheckHexadecimal(input); 
        }

        private bool CheckInteger(string input)
        {
            return input.All(c => char.IsDigit(c));
        }

        private bool CheckBinary(string input)
        {
            return input.Length >= 2 && input.Last() == 'b'
                && input.SkipLast(1).All(c => c == '0' || c == '1');
        }

        private bool CheckOctal(string input)
        {
            List<char> octalDigits = new List<char>
            {
                '0', '1', '2', '3', '4', '5', '6', '7'
            };

            return input.Length >= 2 && input.Last() == 'o'
                && input.SkipLast(1).All(c => octalDigits.Contains(c));
        }

        private bool CheckHexadecimal(string input)
        {
            List<char> hexadecimalDigits = new List<char>
            {
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'A', 'B', 'C', 'D', 'E', 'F'
            };

            return input.Length >= 2 && input.Last() == 'x'
                && input.SkipLast(1).All(c => hexadecimalDigits.Contains(c));
        }

        private bool CheckFloat(string input)
        {
            int pointIndex = input.IndexOf('.');
            if (pointIndex == -1 || pointIndex == 0 || pointIndex == input.Length)
            {
                return false;
            }
            string beforePoint = input.Substring(0, pointIndex);
            string afterPoint = input.Substring(pointIndex + 1, input.Length - pointIndex - 1);
            return beforePoint.All(c => char.IsDigit(c)) && afterPoint.All(c => char.IsDigit(c));
        }
    }
}
