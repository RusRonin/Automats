using Lexer.Extensions;
using System;
using System.IO;

namespace Lexer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string inputFile = args[0];
            using (StreamReader reader = new StreamReader(inputFile))
            {
                Lexer lexer = new Lexer(reader);
                var lexems = lexer.Process();
                for (int i = 0; i < lexems.Count; i++)
                {
                    Console.WriteLine($"{i + 1}: {lexems[i].Type.ToStringValue()} {lexems[i].Data} {lexems[i].Row}:{lexems[i].Column}");
                }
            }
        }
    }
}
