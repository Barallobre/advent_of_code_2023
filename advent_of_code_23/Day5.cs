

using System.Text;

namespace advent_of_code_23
{
    internal class Day5
    {
        
        public static void Solution(List<string> lines) 
        {
            lines.Add("");
            List<string> seeds = new List<string>();   
            List<string> seedToSoil = new List<string>();   
            List<string> soilToFertilizer = new List<string>();   
            List<string> fertilizerToWater = new List<string>();   
            List<string> waterToLight = new List<string>();   
            List<string> lightToTemperature = new List<string>();   
            List<string> temperatureToHumidity = new List<string>();   
            List<string> humidityToLocation = new List<string>();   

            StringBuilder numberBuffer = new StringBuilder();
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Contains("seeds"))
                {
                    SaveNumbers(lines[i], seeds, numberBuffer);
                }
                if (lines[i].Contains("seed-to-soil map:"))
                {
                    while (!string.IsNullOrEmpty(lines[i]))
                    {
                        SaveNumbers(lines[i], seedToSoil, numberBuffer);
                        i++;
                    }
                }
                if (lines[i].Contains("soil-to-fertilizer"))
                {
                    while (!string.IsNullOrEmpty(lines[i]))
                    {
                        SaveNumbers(lines[i], soilToFertilizer, numberBuffer);
                        i++;
                    }
                }
                if (lines[i].Contains("fertilizer-to-water"))
                {
                    while (!string.IsNullOrEmpty(lines[i]))
                    {
                        SaveNumbers(lines[i], fertilizerToWater, numberBuffer);
                        i++;
                    }
                }
                if (lines[i].Contains("water-to-light"))
                {
                    while (!string.IsNullOrEmpty(lines[i]))
                    {
                        SaveNumbers(lines[i], waterToLight, numberBuffer);
                        i++;
                    }
                }
                if (lines[i].Contains("light-to-temperature"))
                {
                    while (!string.IsNullOrEmpty(lines[i]))
                    {
                        SaveNumbers(lines[i], lightToTemperature, numberBuffer);
                        i++;
                    }
                }
                if (lines[i].Contains("temperature-to-humidity"))
                {
                    while (!string.IsNullOrEmpty(lines[i]))
                    {
                        SaveNumbers(lines[i], temperatureToHumidity, numberBuffer);
                        i++;
                    }
                }
                if (lines[i].Contains("humidity-to-location"))
                {
                    while (!string.IsNullOrEmpty(lines[i]) || i < lines.Count-1)
                    {
                        SaveNumbers(lines[i], humidityToLocation, numberBuffer);
                        i++;
                    }
                }

                numberBuffer = new StringBuilder();
            }     

           

        }   
        
        private static void SaveNumbers(string line, List<string> seeds,  StringBuilder numberBuffer)
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

                    seeds.Add(numberBuffer.ToString());
                    numberBuffer = new StringBuilder();
                }
            }
            if (numberBuffer.Length != 0) { seeds.Add(numberBuffer.ToString()); }
        }
    }
}
