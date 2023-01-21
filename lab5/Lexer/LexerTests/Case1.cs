using Lexer;
using Lexer.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LexerTests
{
    public class Case1
    {
        [Fact]
        public void Case1Test()
        {
            string source =
            "a = ( a + 1 );\r\n" +
            "b = \"string\";\r\n" +
            "int a;\r\n" +
            "float a;\r\n" +
            "float a;\r\n" +
            "b = a + ( b * a );\r\n" +
            "{\r\n" +
            "    a = ( a + 1 );\r\n" +
            "    b = \"string\";\r\n" +
            "    int a;\r\n" +
            "    float a;\r\n" +
            "    float a;\r\n" +
            "    b = a + ( b * a );\r\n" +
            "}\r\n" +
            "//comment\r\n" +
            "while ( int i = 0; i = index + 1 )\r\n" +
            "{\r\n" +
            "    for ( var indexI = 0; index < b )\r\n" +
            "    switch ( indexI )\r\n" +
            "        case : 0\r\n" +
            "            indexI = index + 1;\r\n" +
            "            break;\r\n" +
            "        case : 1\r\n" +
            "            indexI = index + 2;\r\n" +
            "            break;\r\n" +
            "        default\r\n" +
            "            return;\r\n" +
            "}";

            var stream = Utils.SourceToStream(source);
            Lexer.Lexer lexer = new Lexer.Lexer(stream);
            StringWriter builder = new StringWriter();
            var oldOut = Console.Out;
            Console.SetOut(builder);
            var lexems = lexer.Process();
            for (int i = 0; i < lexems.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {lexems[i].Type.ToStringValue()} {lexems[i].Data} {lexems[i].Row}:{lexems[i].Column}");
            }
            string output = builder.GetStringBuilder().ToString();
            string expected =
                "1: Identifier a 1:0\r\n" +
                "2: Assigment = 1:2\r\n" +
                "3: Separator ( 1:4\r\n" +
                "4: Identifier a 1:6\r\n" +
                "5: Addition + 1:8\r\n" +
                "6: Number 1 1:10\r\n" +
                "7: Separator ) 1:12\r\n" +
                "8: Separator ; 1:13\r\n" +
                "9: Identifier b 2:0\r\n" +
                "10: Assigment = 2:2\r\n" +
                "11: String \"string\" 2:4\r\n" +
                "12: Separator ; 2:12\r\n" +
                "13: Keyword int 3:0\r\n" +
                "14: Identifier a 3:4\r\n" +
                "15: Separator ; 3:5\r\n" +
                "16: Keyword float 4:0\r\n" +
                "17: Identifier a 4:6\r\n" +
                "18: Separator ; 4:7\r\n" +
                "19: Keyword float 5:0\r\n" +
                "20: Identifier a 5:6\r\n" +
                "21: Separator ; 5:7\r\n" +
                "22: Identifier b 6:0\r\n" +
                "23: Assigment = 6:2\r\n" +
                "24: Identifier a 6:4\r\n" +
                "25: Addition + 6:6\r\n" +
                "26: Separator ( 6:8\r\n" +
                "27: Identifier b 6:10\r\n" +
                "28: Multiplication * 6:12\r\n" +
                "29: Identifier a 6:14\r\n" +
                "30: Separator ) 6:16\r\n" +
                "31: Separator ; 6:17\r\n" +
                "32: Separator { 7:0\r\n" +
                "33: Identifier a 8:0\r\n" +
                "34: Assigment = 8:2\r\n" +
                "35: Separator ( 8:4\r\n" +
                "36: Identifier a 8:6\r\n" +
                "37: Addition + 8:8\r\n" +
                "38: Number 1 8:10\r\n" +
                "39: Separator ) 8:12\r\n" +
                "40: Separator ; 8:17\r\n" +
                "41: Identifier b 9:0\r\n" +
                "42: Assigment = 9:2\r\n" +
                "43: String \"string\" 9:4\r\n" +
                "44: Separator ; 9:16\r\n" +
                "45: Keyword int 10:0\r\n" +
                "46: Identifier a 10:4\r\n" +
                "47: Separator ; 10:9\r\n" +
                "48: Keyword float 11:0\r\n" +
                "49: Identifier a 11:6\r\n" +
                "50: Separator ; 11:11\r\n" +
                "51: Keyword float 12:0\r\n" +
                "52: Identifier a 12:6\r\n" +
                "53: Separator ; 12:11\r\n" +
                "54: Identifier b 13:0\r\n" +
                "55: Assigment = 13:2\r\n" +
                "56: Identifier a 13:4\r\n" +
                "57: Addition + 13:6\r\n" +
                "58: Separator ( 13:8\r\n" +
                "59: Identifier b 13:10\r\n" +
                "60: Multiplication * 13:12\r\n" +
                "61: Identifier a 13:14\r\n" +
                "62: Separator ) 13:16\r\n" +
                "63: Separator ; 13:21\r\n" +
                "64: Separator } 14:0\r\n" +
                "65: Keyword while 16:0\r\n" +
                "66: Separator ( 16:6\r\n" +
                "67: Keyword int 16:8\r\n" +
                "68: Identifier i 16:12\r\n" +
                "69: Assigment = 16:14\r\n" +
                "70: Number 0 16:16\r\n" +
                "71: Separator ; 16:33\r\n" +
                "72: Identifier i 16:19\r\n" +
                "73: Assigment = 16:21\r\n" +
                "74: Identifier index 16:23\r\n" +
                "75: Addition + 16:29\r\n" +
                "76: Number 1 16:31\r\n" +
                "77: Separator ) 16:33\r\n" +
                "78: Separator { 17:0\r\n" +
                "79: Keyword for 18:0\r\n" +
                "80: Separator ( 18:4\r\n" +
                "81: Keyword var 18:6\r\n" +
                "82: Identifier indexI 18:10\r\n" +
                "83: Assigment = 18:17\r\n" +
                "84: Number 0 18:19\r\n" +
                "85: Separator ; 18:36\r\n" +
                "86: Identifier index 18:22\r\n" +
                "87: Comparator < 18:28\r\n" +
                "88: Identifier b 18:30\r\n" +
                "89: Separator ) 18:32\r\n" +
                "90: Keyword switch 19:0\r\n" +
                "91: Separator ( 19:7\r\n" +
                "92: Identifier indexI 19:9\r\n" +
                "93: Separator ) 19:16\r\n" +
                "94: Keyword case 20:0\r\n" +
                "95: Separator : 20:5\r\n" +
                "96: Number 0 20:7\r\n" +
                "97: Identifier indexI 21:0\r\n" +
                "98: Assigment = 21:7\r\n" +
                "99: Identifier index 21:9\r\n" +
                "100: Addition + 21:15\r\n" +
                "101: Number 1 21:17\r\n" +
                "102: Separator ; 21:30\r\n" +
                "103: Keyword break 22:0\r\n" +
                "104: Separator ; 22:17\r\n" +
                "105: Keyword case 23:0\r\n" +
                "106: Separator : 23:5\r\n" +
                "107: Number 1 23:7\r\n" +
                "108: Identifier indexI 24:0\r\n" +
                "109: Assigment = 24:7\r\n" +
                "110: Identifier index 24:9\r\n" +
                "111: Addition + 24:15\r\n" +
                "112: Number 2 24:17\r\n" +
                "113: Separator ; 24:30\r\n" +
                "114: Keyword break 25:0\r\n" +
                "115: Separator ; 25:17\r\n" +
                "116: Keyword default 26:0\r\n" +
                "117: Keyword return 27:0\r\n" +
                "118: Separator ; 27:18\r\n" +
                "119: Separator } 28:0\r\n";
            Assert.Equal(expected, output);
        }
    }
}
