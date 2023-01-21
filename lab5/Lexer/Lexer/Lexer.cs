using Lexer.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lexer
{
    public class Lexer
    {
        private int _column;
        private int _row;
        private StreamReader _input;

        public Lexer(StreamReader input)
        {
            _input = input;
            _column = 0;
            _row = 0;
        }

        public List<Token> Process()
        {
            List<Token> tokens = new List<Token>();
            string line = string.Empty;
            while (!_input.EndOfStream)
            {
                line = _input.ReadLine();
                _row++;

                string[] parts = line.Trim().Split(' ');
                for (int i = 0; i < parts.Length; i++)
                {
                    string part = parts[i];
                    bool hasSemicolon = false;
                    if (part.Equals(string.Empty))
                    {
                        continue;
                    }
                    if (part.Length != 1 && part.Last() == ';')
                    {
                        part = part.Substring(0, part.Length - 1);
                        hasSemicolon = true;
                    }
                    _column = CountColumn(parts, i);
                    Lexem lexem = new Lexem();
                    LexemType type = lexem.DetermineLexemType(part);
                    if (type == LexemType.Comment)
                    {
                        break;
                    }
                    if (type == LexemType.Error)
                    {
                        throw new ApplicationException($"Unknown lexem \"{part}\" at {_row}:{_column}");
                    }

                    tokens.Add(new Token
                    {
                        Type = type.ToTokenType(),
                        Column = _column,
                        Row = _row,
                        Data = part
                    });
                    if (hasSemicolon)
                    {
                        tokens.Add(new Token
                        {
                            Type = TokenType.Separator,
                            Column = line.Length - 1,
                            Row = _row,
                            Data = ";"
                        });
                    }
                }               
            }
            return tokens;
        }

        private int CountColumn(string[] parts, int index)
        {
            int column = 0;
            for (int i = 0; i < index; i++)
            {
                column += parts[i].Length;
            }
            column += index; // whitespaces
            return column;
        }
    }
}
