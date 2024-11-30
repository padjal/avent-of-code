using AdventOfCode.Core.Interfaces;

namespace Task5
{
    internal class Program
    {
        const string INPUT_FILE1 = "../../../input1.txt";
        const string INPUT_FILE2 = "../../../input2.txt";

        static void Main(string[] args)
        {
            ParseInput();
        }

        static void ParseInput()
        {
            var input = File.ReadAllLines(INPUT_FILE1);

            var seeds = input[0].Split(':')[1].Trim().Split(' ').Select(n => int.Parse(n)).ToList();
        }

        public void PartOne()
        {
            throw new NotImplementedException();
        }

        public void PartTwo()
        {
            throw new NotImplementedException();
        }

        class RangeMap
        {
            public int DestinationStart { get; set; }

            public int SourceStart { get; set; }

            public int RangeLength { get; set; }
        }

        class Mapping
        {
            public List<RangeMap> RangeMaps { get; set; } = new List<RangeMap>();

            public string Name { get; set; }
        }
    }
}