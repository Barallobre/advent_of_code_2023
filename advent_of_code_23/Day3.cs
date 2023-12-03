
using System.Text;

namespace advent_of_code_23
{
    internal class Day3
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
                        int previousLine = allTheLines.IndexOf(arrayLine) - 1; //índice línea anterior a la actual
                        int nextLine = allTheLines.IndexOf(arrayLine) + 1; //índice línea posterior a la actual

                        if (arrayLine[indexOfNumber[0]-1] != ".") //comprobar caracter de la misma línea anterior al número
                        {
                            validNumbers.Add(number.ToString());
                        }
     
                        if (arrayLine[indexOfNumber[number.Length-1]+1] != ".") //comprobar caracter de la misma línea posterior al número
                        {
                            validNumbers.Add(number.ToString());
                        }
                        //............................................................
                        string[] prevLine = allTheLines[previousLine];
                        
                        foreach(int n in indexOfNumber)
                        {
                            if (prevLine[n] != "." && !Char.IsDigit(char.Parse(prevLine[n])))
                            {
                                validNumbers.Add(number.ToString());
                            }
                        }
                        if (prevLine[indexOfNumber[0] - 1] != ".") //comprobar caracter de la misma línea anterior al número
                        {
                            validNumbers.Add(number.ToString());
                        }

                        if (prevLine[indexOfNumber[number.Length - 1] + 1] != ".") //comprobar caracter de la misma línea posterior al número
                        {
                            validNumbers.Add(number.ToString());
                        }

                        //............................................................
                        string[] postLine = allTheLines[nextLine];

                        foreach (int n in indexOfNumber)
                        {
                            if (postLine[n] != "." && !Char.IsDigit(char.Parse(postLine[n])))
                            {
                                validNumbers.Add(number.ToString());
                            }
                        }
                        if (postLine[indexOfNumber[0] - 1] != "." && !Char.IsDigit(char.Parse(postLine[indexOfNumber[0] - 1]))) //comprobar caracter de la misma línea anterior al número
                        {
                            validNumbers.Add(number.ToString());
                        }

                        if (postLine[indexOfNumber[number.Length - 1] + 1] != "." && !Char.IsDigit(char.Parse(postLine[indexOfNumber[number.Length - 1] + 1]))) //comprobar caracter de la misma línea posterior al número
                        {
                            validNumbers.Add(number.ToString());
                        }



                        number = new StringBuilder();
                        indexOfNumber.Clear();
                    }
                }

            }

            foreach(var num in validNumbers)
            {
                total += Int32.Parse(num);
            }

            Console.WriteLine(total.ToString());
        }

        private void PreviosAndNextLines()
        {

        }
    }
}
