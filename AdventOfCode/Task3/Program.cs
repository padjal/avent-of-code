using System.Text.RegularExpressions;

namespace Task3
{
    internal class Program
    {
        const string INPUT_FILE = "../../../input2.txt";

        class Coordinate
        {
            public Coordinate(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; set; }
            public int Y { get; set; }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Advent of code - Day 3*");

            var input = ParseInput();

            PartOne(input.Item1, input.Item2);
            PartTwo(input.Item1, input.Item2);
        }

        static (string[], char[][]) ParseInput()
        {
            var lines = File.ReadAllLines(INPUT_FILE);

            var result = new char[lines.Length][];

            for (int i = 0; i<lines.Length; i++)
            {
                result[i] = lines[i].ToCharArray();
            }

            return (lines, result);
        }

        static void PartOne(string[] lines, char[][] schematic)
        {
            var numberRegex = new Regex("[0-9]+");

            var result = 0;

            for (int i = 0; i<lines.Length;i++)
            {
                var numbers = numberRegex.Matches(lines[i]);

                foreach (Match number in numbers)
                {         
                    var startPosition = number.Index;
                    var endPosition = number.Index + number.Length;

                    int j = startPosition;

                    while(j < endPosition)
                    {
                        if (IsEnginePart(i, j, schematic))
                        {
                            result += int.Parse(number.Value);

                            Console.WriteLine($"Number {number.Value} is an engine part.");

                            break;
                        }

                        j++;
                    }
                }
            }

            Console.WriteLine($"Result for part one: {result}");
        }

        static bool IsEnginePart(int x, int y, char[][] schematic)
        {
            // collect all adjascent
            List<char> adjascentSymbols = new();

            if(x > 0 && y > 0) adjascentSymbols.Add(schematic[x - 1][y - 1]);
            if(x > 0) adjascentSymbols.Add(schematic[x - 1][y]);
            if(x > 0 && y < schematic[x].Length - 1) adjascentSymbols.Add(schematic[x - 1][y + 1]);
            if(y > 0) adjascentSymbols.Add(schematic[x][y - 1]);
            if(y < schematic[x].Length - 1) adjascentSymbols.Add(schematic[x][y + 1]);
            if(x < schematic.Length-1 && y > 0) adjascentSymbols.Add(schematic[x + 1][y - 1]);
            if(x < schematic.Length - 1) adjascentSymbols.Add(schematic[x + 1][y]);
            if(x < schematic.Length - 1 && y < schematic[x].Length - 1) adjascentSymbols.Add(schematic[x + 1][y + 1]);

            var symbolRegex = new Regex("[^0-9.]+");

            return symbolRegex.IsMatch(new string(adjascentSymbols.ToArray()));
        }

        static bool IsGear(int x, int y, char[][] schematic)
        {
            // collect all adjascent
            List<char> adjascentSymbols = new();

            if (x > 0 && y > 0) adjascentSymbols.Add(schematic[x - 1][y - 1]);
            if (x > 0) adjascentSymbols.Add(schematic[x - 1][y]);
            if (x > 0 && y < schematic[x].Length - 1) adjascentSymbols.Add(schematic[x - 1][y + 1]);
            if (y > 0) adjascentSymbols.Add(schematic[x][y - 1]);
            if (y < schematic[x].Length - 1) adjascentSymbols.Add(schematic[x][y + 1]);
            if (x < schematic.Length - 1 && y > 0) adjascentSymbols.Add(schematic[x + 1][y - 1]);
            if (x < schematic.Length - 1) adjascentSymbols.Add(schematic[x + 1][y]);
            if (x < schematic.Length - 1 && y < schematic[x].Length - 1) adjascentSymbols.Add(schematic[x + 1][y + 1]);

            var symbolRegex = new Regex("\\*+");

            var adjascent = new string(adjascentSymbols.ToArray());

            return symbolRegex.IsMatch(adjascent);
        }

        static Coordinate HasStar(int x, int y, char[][] schematic)
        {
            // collect all adjascent
            List<char> adjascentSymbols = new();

            if (x > 0 && y > 0 && schematic[x - 1][y - 1] =='*') return new Coordinate(x-1, y-1);
            if (x > 0 && schematic[x - 1][y] == '*') return new Coordinate(x - 1, y);
            if (x > 0 && y < schematic[x].Length - 1 && schematic[x - 1][y + 1] == '*') return new Coordinate(x - 1, y + 1);
            if (y > 0 && schematic[x][y - 1] == '*') return new Coordinate(x, y - 1);
            if (y < schematic[x].Length - 1 && schematic[x][y + 1] == '*') return new Coordinate(x, y + 1);
            if (x < schematic.Length - 1 && y > 0 && schematic[x + 1][y - 1] == '*') return new Coordinate(x + 1, y - 1);
            if (x < schematic.Length - 1 && schematic[x + 1][y] == '*') return new Coordinate(x + 1, y);
            if (x < schematic.Length - 1 && y < schematic[x].Length - 1 && schematic[x + 1][y + 1] == '*') return new Coordinate(x + 1, y + 1);

            return null;
        }

        static void PartTwo(string[] lines, char[][] schematic)
        {
            var numberRegex = new Regex("[0-9]+");

            var dict = new Dictionary<Coordinate, Coordinate>();

            var numberId = 0;

            var result = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                var numbers = numberRegex.Matches(lines[i]);

                foreach (Match number in numbers)
                {
                    var startPosition = number.Index;
                    var endPosition = number.Index + number.Length;

                    int j = startPosition;

                    while (j < endPosition)
                    {
                        var coords = HasStar(i, j, schematic);

                        if (coords != null) {
                            dict[new Coordinate(i, number.Index)] = coords;

                            break;
                        }

                        /*if (IsEnginePart(i, j, schematic))
                        {
                            result += int.Parse(number.Value);

                            Console.WriteLine($"Number {number.Value} is an engine part.");

                            break;
                        }*/

                        j++;
                    }
                }
            }

            var res = dict.GroupBy(d => d.Value);

            Console.WriteLine($"Result for part two: {result}");
        }
    }
}