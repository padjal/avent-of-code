using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace TestCases.Task1
{
    [TestClass]
    public class WrittenToDigitTests
    {
        private Regex regex = new Regex("(one)|(two)|(three)|(four)|(five)|(six)|(seven)|(eight)|(nine)|[0-9]");

        [TestMethod]
        [DataRow("one")]
        [DataRow("two")]
        [DataRow("three")]
        [DataRow("four")]
        [DataRow("five")]
        [DataRow("six")]
        [DataRow("seven")]
        [DataRow("eight")]
        [DataRow("fdsdonenine")]
        [DataRow("sdfsdftwonine")]
        [DataRow("3324nine")]
        [DataRow("xxnine")]
        [DataRow("%$#%nine")]
        [DataRow("sevenine")]
        [DataRow("1s;ldftwo")]
        public void FindMatchInBeginning(string input)
        {
            var matches = regex.Matches(input);

            Assert.IsTrue(matches.FirstOrDefault().Success);

            Console.WriteLine($"Found match {matches.FirstOrDefault().Value} in {input}.");
        }

        [TestMethod]
        [DataRow("one")]
        [DataRow("two")]
        [DataRow("three")]
        [DataRow("four")]
        [DataRow("five")]
        [DataRow("six")]
        [DataRow("seven")]
        [DataRow("eight")]
        [DataRow("nine")]
        [DataRow("sevenine")]
        public void FindMatchInEnd(string input)
        {
            var matches = regex.Matches(input);

            Assert.IsTrue(matches.LastOrDefault().Success);

            Console.WriteLine($"Found match {matches.LastOrDefault().Value} in {input}.");
        }

        [TestMethod]
        public void FindEdgeCases()
        {
            var input = "sevenine";

            var matches = regex.Matches(input);

            Assert.IsTrue(matches.LastOrDefault().Success);
            Assert.IsTrue(matches.LastOrDefault().Value == "nine");

            Console.WriteLine($"Found match {matches.LastOrDefault().Value} in {input}.");
        }
    }
}
