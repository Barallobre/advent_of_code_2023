
using System.Collections;
using System.Text;

namespace advent_of_code_23
{
    internal class Day3_2
    {
        public static void Solution(List<string> lines) 
        {
 
            List<string[]> allTheLines = new List<string[]>();

            CountValidNumbers(SaveLines(allTheLines, lines));

        }

        private static string[] MakeLine(string[] arrayLine, string line) 
        {
            int counter = 1;
            arrayLine[counter-1] = ".";
            foreach (Char c in line)
            {
                arrayLine[counter] = c.ToString();
                counter++;
            }
            arrayLine[counter] = ".";
            return arrayLine;
        }

        private static List<string[]> SaveLines(List<string[]> allTheLines, List<string> lines)
        {
            string[] arrayBlank = new string[lines[0].Length + 2];
           
            for (int i = 0; i < lines[0].Length + 2; i++)
            {
                arrayBlank[i] = ".";
            }
            allTheLines.Add(arrayBlank);

            foreach (string line in lines)
            {
                string[] arrayLine = new string[line.Length + 2];
                    
                arrayLine = MakeLine(arrayLine, line);
                allTheLines.Add(arrayLine);              
            }
            allTheLines.Add(arrayBlank);

            return allTheLines;
        }

        private static void CountValidNumbers(List<string[]> allTheLines)
        {
            StringBuilder number = new StringBuilder();
            List<int> indexOfNumber = new List<int>();
            List<string> validNumbers = new List<string>(); 
            int total = 0;
            int indexAsterisk = 0;
            List<Tuple<string, int, int>> numberAsterisk = new List<Tuple<string, int, int>>();

            foreach (string[] arrayLine in allTheLines) 
            {
                for (int i = 0; i <= arrayLine.Length-1; i++)
                {
                    
                    char character = char.Parse(arrayLine[i]);
                    if (Char.IsDigit(character))
                    {
                        number.Append(character);
                        indexOfNumber.Add(i);
                    }
                    if (!Char.IsDigit(character) && !string.IsNullOrEmpty(number.ToString()))
                    {
                        int previousLine = allTheLines.IndexOf(arrayLine) - 1;
                        int currentLine = allTheLines.IndexOf(arrayLine);
                        int nextLine = allTheLines.IndexOf(arrayLine) + 1;

                        CheckSameLine(arrayLine, indexOfNumber, validNumbers, number, indexAsterisk, currentLine, numberAsterisk);                      

                        string[] prevLine = allTheLines[previousLine];

                        PreviosAndNextLines(prevLine, indexOfNumber, validNumbers, number, indexAsterisk, previousLine, numberAsterisk);

                        string[] postLine = allTheLines[nextLine];

                        PreviosAndNextLines(postLine, indexOfNumber, validNumbers, number, indexAsterisk, nextLine, numberAsterisk);

                        number = new StringBuilder();
                        indexOfNumber.Clear();
                    }
                }
            }

            foreach (var item in numberAsterisk)
            {
                Console.WriteLine(item);
            }
            numberAsterisk.Where(c => c.Item2.Count() > 1 && c.Item3.Count() > 1)

   
        }

        private static void PreviosAndNextLines(string[] typeOfLine, List<int> indexOfNumber, 
            List<string> validNumbers, StringBuilder number, int indexAsterisk, int asteriskLine, List<Tuple<string, int, int>> numberAsterisk)
        {
            foreach (int n in indexOfNumber)
            {
                if (typeOfLine[n] == "*" && !Char.IsDigit(char.Parse(typeOfLine[n])))
                {
                    indexAsterisk = n;
                    CheckValidNumbers(validNumbers, number, indexAsterisk, asteriskLine, numberAsterisk);                   
                }
            }
            if (typeOfLine[indexOfNumber[0] - 1] == "*")
            {
                indexAsterisk = indexOfNumber[0] - 1;
                CheckValidNumbers(validNumbers, number, indexAsterisk, asteriskLine, numberAsterisk);
            }

            if (typeOfLine[indexOfNumber[number.Length - 1] + 1] == "*")
            {
                indexAsterisk = indexOfNumber[number.Length - 1] + 1;
                CheckValidNumbers(validNumbers, number, indexAsterisk, asteriskLine, numberAsterisk);
            }
        }

        private static void CheckSameLine(string[] arrayLine, List<int> indexOfNumber,
            List<string> validNumbers, StringBuilder number, int indexAsterisk, int asteriskLine, List<Tuple<string, int, int>> numberAsterisk)
        {
            if (arrayLine[indexOfNumber[0] - 1] == "*")
            {
                indexAsterisk = indexOfNumber[0] - 1;
                CheckValidNumbers(validNumbers, number, indexAsterisk, asteriskLine, numberAsterisk);
            }

            if (arrayLine[indexOfNumber[number.Length - 1] + 1] == "*")
            {
                indexAsterisk = indexOfNumber[number.Length - 1] + 1;
                CheckValidNumbers(validNumbers, number, indexAsterisk, asteriskLine, numberAsterisk);
            }
        }

        private static void CheckValidNumbers(List<string> validNumbers,StringBuilder number,
            int indexAsterisk, int asteriskLine, List<Tuple<string, int, int>> numberAsterisk)
        {
            List<int> twoNumberList = new List<int>();
            //number, linea asterisco, posicion asterisco
            
            var tupleNumbers = Tuple.Create(number.ToString(), indexAsterisk, asteriskLine);
            numberAsterisk.Add(tupleNumbers);
                


        }
    }
}
