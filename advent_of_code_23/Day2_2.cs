using System.Text;
using System.Text.RegularExpressions;

namespace advent_of_code_23
{
    
    internal class Day2_2
    {
        public static List<int> getDataDay2(string data)
        {
            List<int> inputList = new List<int>();
            List<string> games = new List<string>();
            
            string regexPattern = @"Game ([1-9][0-9]{0,1}(\.[\d]{1,2})?|100): ";

            string game = data.Replace("\r\n", "|");

            string result = Regex.Replace(game, regexPattern, "");

            FormatText(games, result);

            int gameNumber = 0;
            int sum = 0;

            ProblemSolver(games, gameNumber, ref sum);
         
            Console.WriteLine(sum);
            return inputList;
        }    
        
        private static void GetMaxNumbers(ref int maxRed,ref int maxGreen,ref int maxBlue, 
            List<int> red, List<int> blue, List<int> green)
        {
            maxRed = red.Max(r => r);

            maxGreen = green.Max(g => g);

            maxBlue = blue.Max(b => b);
  
        }
        private static void AddColorAndNumberToList(StringBuilder color, StringBuilder number, 
            string item, List<int> red, List<int> blue, List<int> green)
        {
            foreach (Char c in item)
            {

                if (Char.IsDigit(c))
                {
                    number.Append(c);
                }
                if (Char.IsLetter(c))
                {
                    color.Append(c);
                }
                if (';'.Equals(c) || ','.Equals(c) || '|'.Equals(c))
                {
                    CreateLists(color, number, red, blue, green);
                    color = new StringBuilder();
                    number = new StringBuilder();
                }
            }
        }

        private static void CreateLists(StringBuilder color, StringBuilder number, List<int> red, 
            List<int> blue, List<int> green)
        {
            if (color.ToString() == "red")
            {
                red.Add(Int32.Parse(number.ToString()));
            }
            if (color.ToString() == "blue")
            {
                blue.Add(Int32.Parse(number.ToString()));
            }
            if (color.ToString() == "green")
            {
                green.Add(Int32.Parse(number.ToString()));
            }           
        }

        private static void FormatText(List<string> games, string result)
        {
            var line = new StringBuilder();
            foreach (Char c in result)
            {
                line.Append(c);
                if (c == '|')
                {
                    games.Add(line.ToString());
                    line = new StringBuilder();
                }
            }
        }

        private static void ProblemSolver(List<string> games, int gameNumber,ref int sum)
        {
            foreach (string item in games)
            {
                List<int> red = new List<int>();
                List<int> blue = new List<int>();
                List<int> green = new List<int>();

                var color = new StringBuilder();
                var number = new StringBuilder();

                int maxRed = 0;
                int maxGreen = 0;
                int maxBlue = 0;

                AddColorAndNumberToList(color, number, item, red, blue, green);

                GetMaxNumbers(ref maxRed, ref maxGreen, ref maxBlue, red, green, blue);

                red = new List<int>();
                blue = new List<int>();
                green = new List<int>();

                int finalResult = maxRed * maxBlue * maxGreen;

                gameNumber += 1;

                Console.WriteLine(finalResult.ToString());

                sum += finalResult;

                finalResult = 0;
            }
        }
    }
}
