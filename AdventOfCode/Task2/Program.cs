namespace Task2
{
    internal class Program
    {
        const string INPUT_FILE = "../../../input2.txt";
        const int _redCubes = 12;
        const int _greenCubes = 13;
        const int _blueCubes = 14;

        static void Main(string[] args)
        {
            Console.WriteLine("Advent of code - Day 2*");

            var input = ParseInput();

            //PartOne(input);
            PartTwo(input);
        }

        static string[] ParseInput()
        {
            return File.ReadAllLines(INPUT_FILE);
        }

        static void PartOne(string[] input)
        {
            int sumOfGameIds = 0;

            foreach (var line in input)
            {
                var gameId = int.Parse(line.Split(':')[0].Split(' ')[1]);

                var reaches = line.Split(':')[1].Split(';');

                bool isReachLegal = true;

                foreach (var reach in reaches) 
                {
                    var takes = reach.Split(',');

                    int redCubes = 0, greenCubes = 0, blueCubes = 0;

                    foreach (var take in takes)
                    {
                        var numberOfCubes = int.Parse(take.Trim().Split(' ')[0]);
                        
                        var colorOfCubes = take.Trim().Split(' ')[1];

                        switch (colorOfCubes)
                        {
                            case "red":
                                redCubes += numberOfCubes;
                                break;
                            case "green":
                                greenCubes += numberOfCubes;
                                break;
                            default:
                                blueCubes += numberOfCubes;
                                break;
                        }
                    }

                    if (redCubes > _redCubes || greenCubes > _greenCubes || blueCubes > _blueCubes)
                    {
                        isReachLegal = false;

                        Console.WriteLine($"Impossible game id: {gameId}");
                    }
                }

                if (isReachLegal) sumOfGameIds += gameId;
            }

            Console.WriteLine($"PART 1 - The sum of the game ids is {sumOfGameIds}");
        }

        static void PartTwo(string[] input)
        {
            int sumOfPowers = 0;

            foreach (var line in input)
            {
                var gameId = int.Parse(line.Split(':')[0].Split(' ')[1]);

                var reaches = line.Split(':')[1].Split(';');

                bool isReachLegal = true;

                int redCubes = 0, greenCubes = 0, blueCubes = 0;

                foreach (var reach in reaches)
                {
                    var takes = reach.Split(',');


                    foreach (var take in takes)
                    {
                        var numberOfCubes = int.Parse(take.Trim().Split(' ')[0]);

                        var colorOfCubes = take.Trim().Split(' ')[1];

                        switch (colorOfCubes)
                        {
                            case "red":
                                //redCubes += numberOfCubes;
                                if(numberOfCubes > redCubes) redCubes = numberOfCubes;
                                break;
                            case "green":
                                //greenCubes += numberOfCubes;
                                if(numberOfCubes > greenCubes) greenCubes = numberOfCubes;
                                break;
                            default:
                                //blueCubes += numberOfCubes;
                                if(numberOfCubes > blueCubes) blueCubes = numberOfCubes;
                                break;
                        }
                    }
                }

                var gamePower = redCubes * greenCubes * blueCubes;

                Console.WriteLine($"The power of game {gameId} is {gamePower}");

                sumOfPowers += gamePower;
            }

            Console.WriteLine($"PART 2 - The sum of the powers of the games is {sumOfPowers}");
        }
    }
}