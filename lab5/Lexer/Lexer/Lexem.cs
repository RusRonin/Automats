using Lexer.LexemCheckers;
using System.Collections.Generic;

namespace Lexer
{
    public class Lexem
    {
        private List<ILexemChecker> _checkers;

        public Lexem()
        {
            _checkers = new List<ILexemChecker>
            {
                new AdditionChecker(),
                new AssignmentChecker(),
                new CommentChecker(),
                new ComparationChecker(),
                new DivisionChecker(),
                new KeywordChecker(),
                new MultiplicationChecker(),
                new NumberChecker(),
                new SeparatorChecker(),
                new StringChecker(),
                new SubstractionChecker(),
                new IdentifierChecker()
            };
        }

        public LexemType DetermineLexemType(string input)
        {
            foreach (var checker in _checkers)
            {
                if (checker.Check(input))
                {
                    return checker.LexemType;
                }
            }
            return LexemType.Error;
        }
    }
}
