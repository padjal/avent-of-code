using AdventOfCode.Core.Helpers;
using System.Collections;

namespace Task4
{
    internal class Program
    {
        const string INPUT_FILE1 = "../../../input1.txt";
        const string INPUT_FILE2 = "../../../input2.txt";

        static List<Card> cards = new List<Card>();

        static void Main(string[] args)
        {
            cards = GetCards();

            PartOne(cards);

            PartTwo(cards);
        }

        static void PartTwo(List<Card> cards)
        {
            var copies = new Dictionary<int, int>();

            foreach (var card in cards)
            {
                copies[card.Id] = 1;
            }

            AddCoppies(cards, copies);

            var result = copies.Sum(c => c.Value);

            Console.WriteLine($"PART 2 - The sum of all copies is: {result}");
        }

        static void AddCoppies(List<Card> cards, Dictionary<int, int> copies)
        {
            foreach (var card in cards)
            {
                // Trickiest part
                for (var j = 1; j <= card.IntersectingNumbers.Count; j++)
                {
                    copies[card.Id + j] += copies[card.Id];
                }
                             
            }
        }

        static List<Card> GetCards()
        {
            var cards = new List<Card>();

            var input = InputParser.GetLines(INPUT_FILE2);

            foreach (var line in input)
            {
                var cardId = int.Parse(line
                        .Split(':')[0]
                        .Trim()
                        .Split(' ')
                        .Where(s => s != "")
                        .ToList()[1]);

                var numbers = line.Split(":")[1].Trim().Split('|');

                var winningNumbers = numbers[0].Split(" ").Where(n => n != "").Select(n => int.Parse(n)).ToList();
                var guessedNumbers = numbers[1].Split(" ").Where(n => n != "").Select(n => int.Parse(n)).ToList();

                cards.Add(new Card
                {
                    Id = cardId,
                    WinningNumbers = winningNumbers,
                    GuessedNumbers = guessedNumbers
                });
            }            

            return cards;
        }

        static void PartOne(List<Card> cards)
        {
            Console.WriteLine($"PART 1 - The sum of all points is {cards.Sum(c => c.Points)}");
        }
    }

    public class Card
    {
        public int Id { get; set; }

        public List<int> WinningNumbers { get; set; } = new List<int>();

        public List<int> GuessedNumbers { get; set; } = new List<int>();

        public List<int> IntersectingNumbers => WinningNumbers.Intersect(GuessedNumbers).ToList();

        public int Points
        {
            get
            {
                if (IntersectingNumbers.Count == 0) return 0;

                return (int)Math.Pow(2.0, IntersectingNumbers.Count - 1);
            }
        }
    }
}