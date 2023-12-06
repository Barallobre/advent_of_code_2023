

using System;
using System.Reflection;
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
                if (lines[i].Contains("seed-to-soil"))
                {
                    SaveNumbersByType(lines, seedToSoil, numberBuffer, i);
                }
                if (lines[i].Contains("soil-to-fertilizer"))
                {
                    SaveNumbersByType(lines, soilToFertilizer, numberBuffer, i);
                }
                if (lines[i].Contains("fertilizer-to-water"))
                {
                    SaveNumbersByType(lines, fertilizerToWater, numberBuffer, i);
                }
                if (lines[i].Contains("water-to-light"))
                {
                    SaveNumbersByType(lines, waterToLight, numberBuffer, i);
                }
                if (lines[i].Contains("light-to-temperature"))
                {
                    SaveNumbersByType(lines, lightToTemperature, numberBuffer, i);
                }
                if (lines[i].Contains("temperature-to-humidity"))
                {
                    SaveNumbersByType(lines, temperatureToHumidity, numberBuffer, i);
                }
                if (lines[i].Contains("humidity-to-location"))
                {
                    SaveNumbersByType(lines, humidityToLocation, numberBuffer, i);
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

        private static void SaveNumbersByType(List<string> lines, List<string> typeOfNumbers, StringBuilder numberBuffer,int index)
        {
            while (!string.IsNullOrEmpty(lines[index]) || lines[index] != lines[lines.Count() - 1])
            {
                SaveNumbers(lines[index], typeOfNumbers, numberBuffer);
                index++;
            }
        }
    }
}
