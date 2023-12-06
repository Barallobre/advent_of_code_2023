
using System.ComponentModel;
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

            List<Dictionary<int, int>> seedToSoilDictionary = new List<Dictionary<int, int>>();
            List<Dictionary<int, int>> soilToFertilizerDictionary = new List<Dictionary<int, int>>();
            List<Dictionary<int, int>> fertilizerToWaterDictionary = new List<Dictionary<int, int>>();
            List<Dictionary<int, int>> waterToLightDictionary = new List<Dictionary<int, int>>();
            List<Dictionary<int, int>> lightToTemperatureDictionary = new List<Dictionary<int, int>>();
            List<Dictionary<int, int>> temperatureToHumidityDictionary = new List<Dictionary<int, int>>();
            List<Dictionary<int, int>> humidityToLocationDictionary = new List<Dictionary<int, int>>();

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
                    seedToSoilDictionary = AllNumbersByType(seedToSoil);
                }
                if (lines[i].Contains("soil-to-fertilizer"))
                {
                    SaveNumbersByType(lines, soilToFertilizer, numberBuffer, i);
                    soilToFertilizerDictionary = AllNumbersByType(soilToFertilizer);
                }
                if (lines[i].Contains("fertilizer-to-water"))
                {
                    SaveNumbersByType(lines, fertilizerToWater, numberBuffer, i);
                    fertilizerToWaterDictionary = AllNumbersByType(fertilizerToWater);
                }
                if (lines[i].Contains("water-to-light"))
                {
                    SaveNumbersByType(lines, waterToLight, numberBuffer, i);
                    waterToLightDictionary = AllNumbersByType(waterToLight);
                }
                if (lines[i].Contains("light-to-temperature"))
                {
                    SaveNumbersByType(lines, lightToTemperature, numberBuffer, i);
                    lightToTemperatureDictionary = AllNumbersByType(lightToTemperature);
                }
                if (lines[i].Contains("temperature-to-humidity"))
                {
                    SaveNumbersByType(lines, temperatureToHumidity, numberBuffer, i);
                    temperatureToHumidityDictionary = AllNumbersByType(temperatureToHumidity);
                }
                if (lines[i].Contains("humidity-to-location"))
                {
                    SaveNumbersByType(lines, humidityToLocation, numberBuffer, i);
                    humidityToLocationDictionary = AllNumbersByType(humidityToLocation);
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

        private static List<Dictionary<int, int>> AllNumbersByType(List<string> seedToSoil)
        {
            int num1 = 0;
            int num2 = 0;
            int num3 = 0;

            Dictionary<int, int> matchingNumbers = new Dictionary<int, int>();
            List<Dictionary<int, int>> numbersByTypeInDictionary = new List<Dictionary<int, int>>();

            for (int i = 0; i < seedToSoil.Count; i += 3)
            {

                num1 = Int32.Parse(seedToSoil[i]);
                num2 = Int32.Parse(seedToSoil[i + 1]);
                num3 = Int32.Parse(seedToSoil[i + 2]);

                for (int j = 0; j < num3; j++)
                {
                    matchingNumbers.Add(num2 + j, num1 + j);
                }
            }
            numbersByTypeInDictionary.Add(matchingNumbers);

            return numbersByTypeInDictionary;
        }
    }
}
