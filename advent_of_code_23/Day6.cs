
using System.Text;

namespace advent_of_code_23
{
    internal class Day6
    {
        
        public static void Solution(List<string> lines) 
        {
            StringBuilder numberBuffer = new StringBuilder();
            List<int> times = new List<int>();
            List<int> distances = new List<int>();
            for (int i = 0; i < lines.Count; i++)
            {
                if (i == 0)
                {
                    SaveNumbers(lines[i], times, numberBuffer);
                }
                else
                {
                    SaveNumbers(lines[i], distances, numberBuffer);
                }
                
            }
            int distance = 0;
            List<int> waysToWin = new List<int>();
            List<int> distancesResult = new List<int>();

            int waysToWinSum = 0;

            for (int i = 0; i < times.Count; i++)
            {
                for (int j = 0; j <= times[i] ; j++)
                {
                    distance = (times[i] - j) * j;
                    distancesResult.Add(distance);
                    if( distance > distances[i] )
                    {
                        waysToWinSum += 1;
                    }
                    distance = 0;
                }
                waysToWin.Add( waysToWinSum );
                waysToWinSum = 0;
            }

            int total = 1;

            foreach ( var  way in waysToWin)
            {
                total = way * total;
            }

            Console.WriteLine( total );
        }

        private static void SaveNumbers(string line, List<int> seeds, StringBuilder numberBuffer)
        {
            numberBuffer = new StringBuilder();
            foreach (Char c in line)
            {
                if (Char.IsDigit(c))
                {
                    numberBuffer.Append(c);
                }
                else if (numberBuffer.Length != 0)
                {

                    seeds.Add(Int32.Parse(numberBuffer.ToString()));
                    numberBuffer = new StringBuilder();
                }
            }
            if (numberBuffer.Length != 0) { seeds.Add(Int32.Parse(numberBuffer.ToString())); }
        }

    }
}
