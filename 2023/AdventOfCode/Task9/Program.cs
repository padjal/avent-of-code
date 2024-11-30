namespace Task9
{
    internal class Program
    {
        const string INPUT_FILE = "../../../input.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }

        static List<int>[] ParseInput()
        {
            var input = File.ReadLines(INPUT_FILE);

            var values = new List<int>[input.Count()];

            foreach (var line in input)
            {
                var numbers = line.Split(' ').Select(n => int.Parse(n));
            }

            return values;
        }

        static int GetPrediction(IEnumerable<int> numbers)
        {
            for (int i = 0; i < numbers.Count(); i++)
            {

            }

            return 0;
        }
    }
}