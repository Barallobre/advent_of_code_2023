using System.Text;
using System.Text.RegularExpressions;

namespace advent_of_code_23
{
    internal class Day4_2
    {
        
        public static void Solution(List<string> lines) 
        {                    
            List<string> numbersGame = new List<string>();
            List<List<string>> allNumbersGame = new List<List<string>>();

            List<string> myNumbers = new List<string>();
            List<List<string>> allMyNumbers = new List<List<string>>();

            StringBuilder numberBuffer = new StringBuilder();
            
            (allNumbersGame, allMyNumbers) = MappingNumbers(lines, numbersGame, allNumbersGame, myNumbers, allMyNumbers, numberBuffer);
            
            int[] cardsWon = new int[allNumbersGame.Count];
            for (int i = 0; i < cardsWon.Length; i++)
            {
                cardsWon[i] = 1;
            }
            
            CountCards(allNumbersGame, allMyNumbers, ref cardsWon);

            int total = 0;

            foreach (int i in cardsWon) 
            {
                total += i;
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

                SaveNumbers(twoLines[0], numberBuffer, numbersGame);

                allNumbersGame.Add(numbersGame);
                numbersGame = new List<string>();
                numberBuffer = new StringBuilder();

                SaveNumbers(twoLines[1], numberBuffer, myNumbers);

                allMyNumbers.Add(myNumbers);
                myNumbers = new List<string>();
                numberBuffer = new StringBuilder();
            }

            return (allNumbersGame, allMyNumbers);
        }

        private static void CountCards(List<List<string>> allNumbersGame,
            List<List<string>> allMyNumbers, ref int[] cardsWon)
        {
            for (int i = 0; i < allMyNumbers.Count; i++)
            {
                int timesLooping = cardsWon[i];
                
                for (int j = 0; j < timesLooping; j++)
                {
                    int sum = i;
                    foreach (var numberPerCard in allNumbersGame[i])
                    {
                        foreach (var myNumberPerCard in allMyNumbers[i])
                        {
                            if (numberPerCard == myNumberPerCard)
                            {
                                cardsWon[sum + 1] += 1;
                                sum += 1;
                            }
                        }
                    }
                }
            }
        }

        private static void SaveNumbers(string lines, StringBuilder numberBuffer, List<string> numbers)
        {
            foreach (Char c in lines)
            {
                if (Char.IsDigit(c))
                {
                    numberBuffer.Append(c);
                }
                else if (numberBuffer.Length != 0)
                {
                    numbers.Add(numberBuffer.ToString());
                    numberBuffer = new StringBuilder();
                }
            }

            if (numberBuffer.Length != 0) { numbers.Add(numberBuffer.ToString()); }
        }
    }
}
