using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Task7
{
    // Camel cards
    internal class Program
    {
        const string INPUT_FILE = "../../../input.txt";        

        static void Main(string[] args)
        {
            var hands = ParseInput();

            PartOne(hands);
            PartTwo(hands);
        }

        static List<Hand> ParseInput()
        {
            var hands = new List<Hand>();

            var input = File.ReadLines(INPUT_FILE);

            foreach (var line in input)
            {
                hands.Add(new Hand
                {
                    Cards = line.Split(' ')[0],
                    Bet = int.Parse(line.Split(' ')[1])
                });
            }

            return hands;
        }

        static void PartOne(List<Hand> hands)
        {
            var totalWinnings = 0;

            hands.Sort();

            var rank = 1;

            foreach (var hand in hands)
            {
                hand.Rank = rank++;
            }

            totalWinnings = hands.Sum(h => h.Bet * h.Rank);

            Console.WriteLine($"Result of part 1: {totalWinnings}");
        }

        static void PartTwo(List<Hand> hands)
        {
            var totalWinnings = 0;

            hands.ForEach(h => h.Version = 2);

            hands.Sort();

            var rank = 1;

            foreach (var hand in hands)
            {
                hand.Rank = rank++;
            }

            totalWinnings = hands.Sum(h => h.Bet * h.Rank);

            Console.WriteLine($"Result of part 2: {totalWinnings}");
        }

        public enum HandType
        {
            HighCard,
            OnePair,
            TwoPair,
            ThreeOfAKind,
            FullHouse,
            FourOfAkind,
            FiveOfAKind
        }

        public class Hand : IComparable<Hand>
        {
            Dictionary<char, int> _cardStrength = new()
            {
                {'J', 0},
                {'2', 1},
                {'3', 2},
                {'4', 3},
                {'5', 4},
                {'6', 5},
                {'7', 6},
                {'8', 7},
                {'9', 8},
                {'T', 9},
                {'Q', 11},
                {'K', 12},
                {'A', 13}
            };

            public string Cards { get; set; } = string.Empty;

            public int Bet { get; set; }

            public int Version { get; set; }

            public int Rank { get; set; }

            public HandType Type
            {
                get
                {
                    var cardGroups = Cards.GroupBy(c => c);

                    if (Version == 2)
                    {
                        // All J's turn into the most common char

                        cardGroups = cardGroups.Where(c => c.Key != 'J');

                        //Edge case
                        if(cardGroups.Count() == 0)
                        {
                            return HandType.FiveOfAKind;
                        }

                        var maxOccurrences = cardGroups.Max(c => c.Count());

                        var mostCommonChars = cardGroups.Where(c => c.Count() == maxOccurrences);

                        var mostCommonChar = mostCommonChars.MaxBy(c => _cardStrength[c.Key]).ElementAt(0);

                        var changedCards = Cards.Replace('J', mostCommonChar);

                        cardGroups = changedCards.GroupBy(c => c);
                    }

                    if (cardGroups.Where(g => g.Count() == 5).Count() > 0) 
                        return HandType.FiveOfAKind;
                    if (cardGroups.Where(g => g.Count() == 4).Count() > 0)
                        return HandType.FourOfAkind;
                    if (cardGroups.Where(g => g.Count() == 3).Count() > 0 
                        && cardGroups.Where(g => g.Count() == 2).Count() > 0)
                        return HandType.FullHouse;
                    if (cardGroups.Where(g => g.Count() == 3).Count() > 0)
                        return HandType.ThreeOfAKind;
                    if (cardGroups.Where(g => g.Count() == 2).Count() > 1)
                        return HandType.TwoPair;
                    if (cardGroups.Where(g => g.Count() == 2).Count() > 0)
                        return HandType.OnePair;
                    return HandType.HighCard;
                }
            }

            
            int IComparable<Hand>.CompareTo(Hand? other)
            {
                if (other == null) return 1;

                if (Type > other.Type) return 1;

                if (Type < other.Type) return -1;

                // (Type == other.Type)
                for (int i = 0; i < Cards.Length; i++)
                {
                    if (_cardStrength[Cards[i]] == _cardStrength[other.Cards[i]])
                            continue;

                    if(_cardStrength[Cards[i]] > _cardStrength[other.Cards[i]])
                        return 1;

                    return -1;
                }

                return 0;
            }
        }
    }
}