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

            var game = data.Replace("\r\n", "|");

            var result = Regex.Replace(game, regexPattern, "");

            var line = new StringBuilder();
            foreach(Char c in result)
            {
                line.Append(c);
                if (c == '|') 
                { 
                    games.Add(line.ToString());
                    line = new StringBuilder();
                }
            }
            var gameNumber = 0;
            var sum = 0;

            foreach (var item in games)
            {
                List<int> red = new List<int>();
                List<int> blue = new List<int>();
                List<int> green = new List<int>();
 
                var color = new StringBuilder();
                var number = new StringBuilder();

                foreach(Char c in item)
                {
                    
                    if (Char.IsDigit(c))
                    {
                        number.Append(c);
                    }
                    if (Char.IsLetter(c))
                    {
                        color.Append(c);
                    }
                    if(';'.Equals(c) || ','.Equals(c) || '|'.Equals(c))
                    {
                       if(color.ToString() == "red")
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

                        color = new StringBuilder();
                        number = new StringBuilder();
                    }

                }

                int maxRed = 0;
                int maxGreen = 0;
                int maxBlue = 0;

                foreach (var n in red)
                {
                    if (n > maxRed)
                    {
                        maxRed = n;
                    }
                }
                foreach (var n in green)
                {
                    if (n > maxGreen)
                    {
                        maxGreen = n;
                    }
                }
                foreach (var n in blue)
                {
                    if (n > maxBlue)
                    {
                        maxBlue = n;
                    }
                }

                red = new List<int>();
                blue = new List<int>();
                green = new List<int>();

                int finalResult = maxRed * maxBlue * maxGreen;

                gameNumber += 1;

                Console.WriteLine(finalResult.ToString());

                sum += finalResult;

                finalResult = 0;

                
            }
            Console.WriteLine(sum);
            return inputList;
        }        
    }
}
