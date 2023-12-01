using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Task1;

namespace AdventOfCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string line;
            int sum = 0;

            while(!string.IsNullOrEmpty(line = Console.ReadLine()))
            {

                var testNumberRegex = new Regex("(one)|(two)|(three)|(four)|(five)|(six)|(seven)|(eight)|(nine)|[0-9]");
                var testNumberRegexFromRight = new Regex("(one)|(two)|(three)|(four)|(five)|(six)|(seven)|(eight)|(nine)|[0-9]", RegexOptions.RightToLeft);

                var leftNumber = testNumberRegex.Match(line).Value;
                var rightNumber = testNumberRegexFromRight.Match(line).Value;

                if (leftNumber.Length != 1) leftNumber = leftNumber.ToDigit();
                if (rightNumber.Length != 1) rightNumber = rightNumber.ToDigit();

                int.TryParse(leftNumber
                    + rightNumber, out int result);

                Console.WriteLine(result);

                sum += result;
            }

            Console.WriteLine(sum);
        }
    }
}