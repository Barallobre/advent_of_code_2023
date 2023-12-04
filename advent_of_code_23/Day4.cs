using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;

namespace advent_of_code_23
{
    internal class Day4
    {
        public static void Solution(List<string> lines) 
        {
            string regexPattern = @"[1-9][0-9]{0,2}: ";
            
            List<string> numbersGame = new List<string>();
            List<List<string>> allNumbersGame = new List<List<string>>();

            List<string> myNumbers = new List<string>();
            List<List<string>> allMyNumbers = new List<List<string>>();

            StringBuilder numberBuffer = new StringBuilder();
  
            foreach (string line in lines)
            {
                string cleanedLine = Regex.Replace(line, regexPattern, "").Replace("Card ","");
                
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
        }        
    }
}
