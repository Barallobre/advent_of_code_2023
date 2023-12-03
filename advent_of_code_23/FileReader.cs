
namespace advent_of_code_23
{
    internal class FileReader
    {
        public static void Reader(string file,ref List<string> lines) 
        {
            lines = new List<string>();
            using (StreamReader reader = new StreamReader(file))
            {
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    Writer(line, lines);
                }
            }
        }

        private static void Writer(string line, List<string> lines)
        {           
            lines.Add(line);
        }
    }
}
