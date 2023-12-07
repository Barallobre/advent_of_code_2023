using System.Numerics;
using System.Runtime.InteropServices;
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

            List<(long, long)> seedToSoilTuple = new List<(long, long)>();
            List<(long, long)> soilToFertilizerTuple = new List<(long, long)>();
            List<(long, long)> fertilizerToWaterTuple = new List<(long, long)>();
            List<(long, long)> waterToLightTuple = new List<(long, long)>();
            List<(long, long)> lightToTemperatureTuple = new List<(long, long)>();
            List<(long, long)> temperatureToHumidityTuple = new List<(long, long)>();
            List<(long, long)> humidityToLocationTuple = new List<(long, long)>();

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
                    seedToSoilTuple = AllNumbersByType(seedToSoil);
                }
                if (lines[i].Contains("soil-to-fertilizer"))
                {
                    SaveNumbersByType(lines, soilToFertilizer, numberBuffer, i);
                    soilToFertilizerTuple = AllNumbersByType(soilToFertilizer);
                }
                if (lines[i].Contains("fertilizer-to-water"))
                {
                    SaveNumbersByType(lines, fertilizerToWater, numberBuffer, i);
                    fertilizerToWaterTuple = AllNumbersByType(fertilizerToWater);
                }
                if (lines[i].Contains("water-to-light"))
                {
                    SaveNumbersByType(lines, waterToLight, numberBuffer, i);
                    waterToLightTuple = AllNumbersByType(waterToLight);
                }
                if (lines[i].Contains("light-to-temperature"))
                {
                    SaveNumbersByType(lines, lightToTemperature, numberBuffer, i);
                    lightToTemperatureTuple = AllNumbersByType(lightToTemperature);
                }
                if (lines[i].Contains("temperature-to-humidity"))
                {
                    SaveNumbersByType(lines, temperatureToHumidity, numberBuffer, i);
                    temperatureToHumidityTuple = AllNumbersByType(temperatureToHumidity);
                }
                if (lines[i].Contains("humidity-to-location"))
                {
                    SaveNumbersByType(lines, humidityToLocation, numberBuffer, i);
                    humidityToLocationTuple = AllNumbersByType(humidityToLocation);
                }

                numberBuffer = new StringBuilder();
            }

            List<long?> locations = new List<long?>();
            LooKForMatchingNumbers(seedToSoilTuple, soilToFertilizerTuple, fertilizerToWaterTuple, waterToLightTuple,
                lightToTemperatureTuple, temperatureToHumidityTuple, humidityToLocationTuple, seeds, locations);


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

        private static List<(long, long)> AllNumbersByType(List<string> seedToSoil)
        {
            long num1 = 0;
            long num2 = 0;
            long num3 = 0;


            Tuple<long, long> tup = new Tuple<long, long>(0, 0);
            List <(long, long)> numbersByTypeInTupleList = new List<(long, long)>();

            for (int i = 0; i < seedToSoil.Count; i += 3)
            {

                num1 = long.Parse(seedToSoil[i]);
                num2 = long.Parse(seedToSoil[i + 1]);
                num3 = long.Parse(seedToSoil[i + 2]);

                for (int j = 0; j < num3; j++)
                {
                    tup = new Tuple<long, long>(num2 + j, num1 + j);
                    numbersByTypeInTupleList.Add((tup.Item1, tup.Item2));
                }
            }
            
            

            return numbersByTypeInTupleList;
        }

        private static void LooKForMatchingNumbers(
            List<(long, long)> seedToSoilTuple, 
            List<(long, long)> soilToFertilizerTuple,
            List<(long, long)> fertilizerToWaterTuple, 
            List<(long, long)> waterToLightTuple,
            List<(long, long)> lightToTemperatureTuple,
            List<(long, long)> temperatureToHumidityTuple,
            List<(long, long)> humidityToLocationTuple, 
            List<string> seeds, 
            List<long?> locations)
        {
            var watch = new System.Diagnostics.Stopwatch();

            watch.Start();
    

            foreach(var seed in seeds)
            {
                


                long? soilValue = null;

                foreach (var key in seedToSoilTuple)
                {
                    if (key.Item1 == long.Parse(seed))
                    {
                        soilValue = key.Item2;
                        break;
                    }
                }

                long? soilFinalValue = soilValue is not null ? soilValue : long.Parse(seed);


                long? fertilizerValue = null;

                foreach (var key in soilToFertilizerTuple)
                {
                    if (key.Item1 == soilFinalValue)
                    {
                        fertilizerValue = key.Item2;
                        break;
                    }
                }

                long? fertilizerFinalValue = fertilizerValue is not null ? fertilizerValue : soilFinalValue;


                long? waterValue = null;

                foreach (var key in fertilizerToWaterTuple)
                {
                    if (key.Item1 == fertilizerFinalValue)
                    {
                        waterValue = key.Item2;
                        break;
                    }
                }

                long? waterFinalValue = waterValue is not null ? waterValue : fertilizerFinalValue;


                long? lightValue = null;

                foreach (var key in waterToLightTuple)
                {
                    if (key.Item1 == waterFinalValue)
                    {
                        lightValue = key.Item2;
                        break;
                    }
                }

                long? lightFinalValue = lightValue is not null ? lightValue : waterFinalValue;


                long? temperatureValue = null;

                foreach (var key in lightToTemperatureTuple)
                {
                    if (key.Item1 == lightFinalValue)
                    {
                        temperatureValue = key.Item2;
                        break;
                    }
                }

                long? temperatureFinalValue = temperatureValue is not null ? temperatureValue : lightFinalValue;


                long? humidityValue = null;

                foreach (var key in temperatureToHumidityTuple)
                {
                    if (key.Item1 == temperatureFinalValue)
                    {
                        humidityValue = key.Item2;
                        break;
                    }
                }

                long? humidityFinalValue = humidityValue is not null ? humidityValue : temperatureFinalValue;


                long? locationValue = null;

                foreach (var key in humidityToLocationTuple)
                {
                    if (key.Item1 == humidityFinalValue)
                    {
                        locationValue = key.Item2;
                        break;
                    }
                }

                long? locationFinalValue = locationValue is not null ? locationValue : humidityFinalValue;


                locations.Add(locationFinalValue);
            }

            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.Elapsed.TotalMilliseconds} ms");

        }
    }
}
