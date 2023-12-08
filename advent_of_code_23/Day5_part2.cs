using System.Text;

namespace advent_of_code_23
{
    internal class Day5_part2
    {

        public static async void Solution(List<string> lines)
        {
            lines.Add("");
            List<long> seeds = new List<long>();
            List<long> seedToSoil = new List<long>();
            List<long> soilToFertilizer = new List<long>();
            List<long> fertilizerToWater = new List<long>();
            List<long> waterToLight = new List<long>();
            List<long> lightToTemperature = new List<long>();
            List<long> temperatureToHumidity = new List<long>();
            List<long> humidityToLocation = new List<long>();


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
            List<long?> locations = new List<long?>();

            await AsyncMethod(seedToSoil, soilToFertilizer, fertilizerToWater, waterToLight,
                lightToTemperature, temperatureToHumidity, humidityToLocation, locations, seeds).ConfigureAwait(true);

            long? lowestLocation = long.MaxValue;

            foreach (var location in locations) 
            {
                if(location < lowestLocation)
                {
                    lowestLocation = location;
                }
            }
            Console.WriteLine(locations.Count + " number of locations");
            Console.WriteLine(lowestLocation.ToString() + " lowest");
        }

        private static Task<bool> AsyncMethod(List<long> seedToSoil,
        List<long> soilToFertilizer,
        List<long> fertilizerToWater,
        List<long> waterToLight,
        List<long> lightToTemperature,
        List<long> temperatureToHumidity,
        List<long> humidityToLocation,
        List<long?> locations,
        List<long> seeds)
        {
            for (int i = 0; i < seeds.Count; i += 2)
            {

                long num1 = seeds[i];
                long num2 = seeds[i + 1];
                var watch = new System.Diagnostics.Stopwatch();

                watch.Start();
                Thread.CurrentThread.IsBackground = true;
                new Thread(() =>
                {
                    LoopSeeds(seedToSoil, soilToFertilizer, fertilizerToWater, waterToLight,
                lightToTemperature, temperatureToHumidity, humidityToLocation, num1, num2, locations);

                }).Start();

                watch.Stop();
                Console.WriteLine($"Execution Time: {watch.Elapsed.TotalMilliseconds} ms");

            }
            return Task.FromResult(true);
        }



        private static void LoopSeeds(
        List<long> seedToSoil,
        List<long> soilToFertilizer,
        List<long> fertilizerToWater,
        List<long> waterToLight,
        List<long> lightToTemperature,
        List<long> temperatureToHumidity,
        List<long> humidityToLocation,
        long num1,
        long num2,
        List<long?> locations)
        {
            for (int j = 0; j < num2; j++)
            {
                long? soilValue = GetValues(num1 + j, seedToSoil);
                long? ferilizerValue = GetValues(soilValue, soilToFertilizer);
                long? waterValue = GetValues(ferilizerValue, fertilizerToWater);
                long? lightValue = GetValues(waterValue, waterToLight);
                long? temperatureValue = GetValues(lightValue, lightToTemperature);
                long? humidityValue = GetValues(temperatureValue, temperatureToHumidity);
                long? locationValue = GetValues(humidityValue, humidityToLocation);

                locations.Add(locationValue);
                Console.WriteLine(locationValue.ToString());

            } 
           
        }

        private static long? GetValues(long? previousValue, List<long> seedToSoil)
        {
            long? soil = null;


            for (int i = 0; i < seedToSoil.Count; i += 3)
            {

                long num1 = seedToSoil[i];
                long num2 = seedToSoil[i + 1];
                long num3 = seedToSoil[i + 2];


                if (previousValue >= num2 && previousValue <= (num2 + num3 - 1))
                {
                    long difference = (long)Math.Abs(Convert.ToDecimal(previousValue) - num2);
                    soil = difference + num1;
                }
            }
            if (soil == null)
            {
                soil = previousValue;
            }

            return soil;
        }

        private static void SaveNumbers(string line, List<long> listOfNumbers, StringBuilder numberBuffer)
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

                    listOfNumbers.Add(long.Parse(numberBuffer.ToString()));
                    numberBuffer = new StringBuilder();
                }
            }
            if (numberBuffer.Length != 0) { listOfNumbers.Add(long.Parse(numberBuffer.ToString())); }
        }

        private static void SaveNumbersByType(List<string> lines, List<long> typeOfNumbers, StringBuilder numberBuffer, int index)
        {
            while (!string.IsNullOrEmpty(lines[index]) || lines[index] != lines[lines.Count() - 1])
            {
                SaveNumbers(lines[index], typeOfNumbers, numberBuffer);
                index++;
            }
        }
    }
}
