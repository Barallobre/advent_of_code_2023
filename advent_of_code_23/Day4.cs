using System.Text;
using System.Text.RegularExpressions;

namespace advent_of_code_23
{
    internal class Day4
    {
        
        public static void Solution(List<string> lines) 
        {
            
            
            List<string> numbersGame = new List<string>();
            List<List<string>> allNumbersGame = new List<List<string>>();

            List<string> myNumbers = new List<string>();
            List<List<string>> allMyNumbers = new List<List<string>>();

            StringBuilder numberBuffer = new StringBuilder();
            
            (allNumbersGame, allMyNumbers) = MappingNumbers(lines, numbersGame, allNumbersGame, myNumbers, allMyNumbers, numberBuffer);

            double power = 0;
            double total = 0;
            double sum = 0;
            for (int i = 0; i < allMyNumbers.Count; i++)
            {
                foreach (var numberPerCard in allNumbersGame[i])
                {
                    foreach (var myNumberPerCard in allMyNumbers[i])
                    {
                        if (numberPerCard ==myNumberPerCard)
                        {
                            power++;
                        }
                    }
                }
                sum = power == 0 ? 0 : Math.Pow(2, power) / 2;
                total += sum;
                power = 0;
            }

            Console.WriteLine(total);           
        }   
        
        private static (List<List<string>>, List<List<string>>) MappingNumbers(
            List<string> lines, List<string> numbersGame, List<List<string>> allNumbersGame,
            List<string> myNumbers, List<List<string>> allMyNumbers, StringBuilder numberBuffer)
        {
            string regexPattern = @"[1-9][0-9]{0,2}: ";

            foreach (string line in lines)
            {
                string cleanedLine = Regex.Replace(line, regexPattern, "").Replace("Card ", "");

                string[] twoLines = cleanedLine.Split('|');

                foreach (Char c in twoLines[0])
                {
                    if (Char.IsDigit(c))
                    {
                        numberBuffer.Append(c);
                    }
                    else if (numberBuffer.Length != 0)
                    {
                        numbersGame.Add(numberBuffer.ToString());
                        numberBuffer = new StringBuilder();
                    }
                }
                allNumbersGame.Add(numbersGame);
                numbersGame = new List<string>();
                numberBuffer = new StringBuilder();

                foreach (Char c in twoLines[1])
                {
                    if (Char.IsDigit(c))
                    {
                        numberBuffer.Append(c);
                    }
                    else if (numberBuffer.Length != 0)
                    {
                        myNumbers.Add(numberBuffer.ToString());
                        numberBuffer = new StringBuilder();
                    }
                }
                if (numberBuffer.Length != 0) { myNumbers.Add(numberBuffer.ToString()); }
                allMyNumbers.Add(myNumbers);
                myNumbers = new List<string>();
                numberBuffer = new StringBuilder();

            }

            return (allNumbersGame, allMyNumbers);
        }
    }
}
