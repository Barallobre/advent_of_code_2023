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

        private static void CountCards(List<List<string>> allNumbersGame,
            List<List<string>> allMyNumbers, ref int[] cardsWon, int index = 0)
        {
            int sum = 0;
              
            int timesLooping = cardsWon[index];

            for (int i = 0; i < timesLooping; i++)
            {
                sum = index;
                foreach (var numberPerCard in allNumbersGame[index])
                {
                    foreach (var myNumberPerCard in allMyNumbers[index])
                    {
                        if (numberPerCard == myNumberPerCard)
                        {
                            cardsWon[sum + 1] += 1;
                            sum += 1;
                        }
                    }
                }               
            }

            int endRecursion = index + 1;
            
            while (endRecursion < allMyNumbers.Count)
            {
                index++;
                CountCards(allNumbersGame, allMyNumbers, ref cardsWon, index);
            }

            if (endRecursion == allMyNumbers.Count)
            {
                int total = 0;
                foreach(var number in cardsWon)
                {
                    total += number;
                }
                Console.WriteLine(total.ToString());
                System.Environment.Exit(0);
            }
        }           
    }
}
