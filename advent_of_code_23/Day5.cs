using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
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

            List<Dictionary<BigInteger, BigInteger>> seedToSoilDictionary = new List<Dictionary<BigInteger, BigInteger>>();
            List<Dictionary<BigInteger, BigInteger>> soilToFertilizerDictionary = new List<Dictionary<BigInteger, BigInteger>>();
            List<Dictionary<BigInteger, BigInteger>> fertilizerToWaterDictionary = new List<Dictionary<BigInteger, BigInteger>>();
            List<Dictionary<BigInteger, BigInteger>> waterToLightDictionary = new List<Dictionary<BigInteger, BigInteger>>();
            List<Dictionary<BigInteger, BigInteger>> lightToTemperatureDictionary = new List<Dictionary<BigInteger, BigInteger>>();
            List<Dictionary<BigInteger, BigInteger>> temperatureToHumidityDictionary = new List<Dictionary<BigInteger, BigInteger>>();
            List<Dictionary<BigInteger, BigInteger>> humidityToLocationDictionary = new List<Dictionary<BigInteger, BigInteger>>();

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

            List<BigInteger> locations = new List<BigInteger>();
            LooKForMatchingNumbers(seedToSoilDictionary, soilToFertilizerDictionary, fertilizerToWaterDictionary, waterToLightDictionary,
                lightToTemperatureDictionary, temperatureToHumidityDictionary, humidityToLocationDictionary, seeds, locations);


            var lowestLocation = locations.Min();
            Console.WriteLine(lowestLocation.ToString());



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

        private static List<Dictionary<BigInteger, BigInteger>> AllNumbersByType(List<string> seedToSoil)
        {
            BigInteger num1 = 0;
            BigInteger num2 = 0;
            BigInteger num3 = 0;

            Dictionary<BigInteger, BigInteger> matchingNumbers = new Dictionary<BigInteger, BigInteger>();
            List<Dictionary<BigInteger, BigInteger>> numbersByTypeInDictionary = new List<Dictionary<BigInteger, BigInteger>>();

            for (int i = 0; i < seedToSoil.Count; i += 3)
            {

                num1 = BigInteger.Parse(seedToSoil[i]);
                num2 = BigInteger.Parse(seedToSoil[i + 1]);
                num3 = BigInteger.Parse(seedToSoil[i + 2]);

                for (int j = 0; j < num3; j++)
                {
                    matchingNumbers.Add(num2 + j, num1 + j);
                }
            }
            numbersByTypeInDictionary.Add(matchingNumbers);

            return numbersByTypeInDictionary;
        }

        private static void LooKForMatchingNumbers(
            List<Dictionary<BigInteger, BigInteger>> seedToSoilDictionary, 
            List<Dictionary<BigInteger, BigInteger>> soilToFertilizerDictionary,
            List<Dictionary<BigInteger, BigInteger>> fertilizerToWaterDictionary, 
            List<Dictionary<BigInteger, BigInteger>> waterToLightDictionary,
            List<Dictionary<BigInteger, BigInteger>> lightToTemperatureDictionary,
            List<Dictionary<BigInteger, BigInteger>> temperatureToHumidityDictionary,
            List<Dictionary<BigInteger, BigInteger>> humidityToLocationDictionary, 
            List<string> seeds, 
            List<BigInteger> locations)
        {

            

            foreach (var seed in seeds)
            {
                var soil = seedToSoilDictionary.SelectMany(k => k)
                    .Where(key => key.Key == BigInteger.Parse(seed)).ToList();
                Console.WriteLine(soil.ToString());
                var soilValue = soil.FirstOrDefault().Value == 0 ? BigInteger.Parse(seed) : soil.FirstOrDefault().Value;
                Console.WriteLine(soilValue.ToString());
                var fertilizer = soilToFertilizerDictionary.SelectMany(k => k)
                    .Where(key => key.Key == soilValue).ToList();
                
                var fertilizerValue = fertilizer.FirstOrDefault().Value == 0 ? soilValue : fertilizer.FirstOrDefault().Value;
                Console.WriteLine(fertilizerValue.ToString());
                var water = fertilizerToWaterDictionary.SelectMany(k => k)
                    .Where(key => key.Key == fertilizerValue).ToList();

                var waterValue = water.FirstOrDefault().Value == 0 ? fertilizerValue : water.FirstOrDefault().Value;
                Console.WriteLine(waterValue.ToString());
                var light = waterToLightDictionary.SelectMany(k => k)
                    .Where(key => key.Key == waterValue).ToList();

                var lightValue = light.FirstOrDefault().Value == 0 ? waterValue : light.FirstOrDefault().Value;
                Console.WriteLine(lightValue.ToString());
                var temperature = lightToTemperatureDictionary.SelectMany(k => k)
                    .Where(key => key.Key == lightValue).ToList();

                var temperatureValue = temperature.FirstOrDefault().Value == 0 ? lightValue : temperature.FirstOrDefault().Value;
                Console.WriteLine(temperatureValue.ToString());
                var humidity = temperatureToHumidityDictionary.SelectMany(k => k)
                    .Where(key => key.Key == temperatureValue).ToList();

                var humidityValue = humidity.FirstOrDefault().Value == 0 ? temperatureValue : humidity.FirstOrDefault().Value;
                Console.WriteLine(humidityValue.ToString());
                var location = humidityToLocationDictionary.SelectMany(k => k)
                    .Where(key => key.Key == humidityValue).ToList();

                var locationValue = location.FirstOrDefault().Value == 0 ? humidityValue : location.FirstOrDefault().Value;
                Console.WriteLine(locationValue.ToString());

                locations.Add(locationValue);
            }
        }
    }
}
