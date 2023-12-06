using System.Numerics;
using System.Text.RegularExpressions;

namespace Task6
{
    internal class Program
    {
        const string INPUT_FILE = "../../../input2.txt";

        // Determine the numeber of ways you can beath the race
        static void Main(string[] args)
        {
            PartOne();

            PartTwo();
        }

        static List<Race> ParseInputPartOne()
        {
            var races = new List<Race>();

            var input = File.ReadLines(INPUT_FILE).ToArray();

            var numberRegex = new Regex("[0-9]+");

            var times = numberRegex.Matches(input[0]).Select(m => int.Parse(m.Value)).ToArray();
            var distances = numberRegex.Matches(input[1]).Select(m => int.Parse(m.Value)).ToArray();

            for (int i = 0; i < times.Count(); i++)
            {
                races.Add(new Race(times[i], distances[i]));
            }

            return races;
        }

        class Race
        {
            public Race(BigInteger time, BigInteger recordDistance)
            {
                Time = time;
                RecordDistance = recordDistance;
            }

            public BigInteger Time { get; set; }

            public BigInteger RecordDistance { get; set; }

            public BigInteger WaysToWin { get; set; }
        }

        static void PartOne()
        {
            var races = ParseInputPartOne();

            // Initial speed 0mm/sec

            // Simulate all possible variants

            foreach(var race in races)
            {
                for(int i = 0; i < race.Time; i++)
                {
                    var speed = i;

                    if(speed*(race.Time - i) > race.RecordDistance)
                    {
                        // we have found a record-breaking speed.

                        race.WaysToWin++;
                    }
                }
            }

            BigInteger result = 1;

            races.ForEach(r => result *= r.WaysToWin);

            Console.WriteLine($"Result of part 1: {result}");
        }

        static void PartTwo()
        {
            var race = ParseInputPartTwo();

            // Initial speed 0mm/sec

            // Simulate all possible variants

            for (int i = 0; i < race.Time; i++)
            {
                var speed = i;

                if (speed * (race.Time - i) > race.RecordDistance)
                {
                    // we have found a record-breaking speed.

                    race.WaysToWin++;
                }
            }

            Console.WriteLine($"Result of part 2: {race.WaysToWin}");
        }

        private static Race ParseInputPartTwo()
        {
            var races = new List<Race>();

            var input = File.ReadLines(INPUT_FILE).ToArray();

            var numberRegex = new Regex("[0-9]+");

            var times = string.Empty;
            var distances = string.Empty;

            numberRegex.Matches(input[0]).ToList().ForEach(match =>  times += match.Value );
            numberRegex.Matches(input[1]).ToList().ForEach(match =>  distances += match.Value );

            return new Race(BigInteger.Parse(times), BigInteger.Parse(distances));
        }
    }
}