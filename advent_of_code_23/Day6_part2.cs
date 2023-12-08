
using System.Text;

namespace advent_of_code_23
{
    internal class Day6_part2
    {
        
        public static void Solution(List<string> lines) 
        {
            StringBuilder numberBuffer = new StringBuilder();
            List<long> times = new List<long>();
            List<long> distances = new List<long>();
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
            long distance = 0;
            List<long> waysToWin = new List<long>();
            List<long> distancesResult = new List<long>();

            int waysToWinSum = 0;

            for (int j = 0; j <= times[0] ; j++)
            {
                distance = (times[0] - j) * j;
                distancesResult.Add(distance);
                if( distance > distances[0] )
                {
                    waysToWinSum += 1;
                }
                distance = 0;
            }
            waysToWin.Add( waysToWinSum );



            long total = 1;

            foreach ( var  way in waysToWin)
            {
                total = way * total;
            }

            Console.WriteLine( total );
        }

        private static void SaveNumbers(string line, List<long> seeds, StringBuilder numberBuffer)
        {
            numberBuffer = new StringBuilder();
            foreach (Char c in line)
            {
                if (Char.IsDigit(c))
                {
                    numberBuffer.Append(c);
                }              
            }
            if (numberBuffer.Length != 0) { seeds.Add(long.Parse(numberBuffer.ToString())); }
        }

    }
}
