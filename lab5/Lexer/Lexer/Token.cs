namespace Lexer
{
    public class Token
    {
        public TokenType Type { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        public string Data { get; set; }
    }
}
