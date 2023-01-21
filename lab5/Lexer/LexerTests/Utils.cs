using System.IO;

namespace LexerTests
{
    public static class Utils
    {

        public static StreamReader SourceToStream(string source)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(source);
            writer.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            return reader;
        }
    }
}
