using System.IO;

namespace AdventOfCode.Core.Helpers
{
    public static class InputParser
    {
        public static string[] GetLines(string filePath)
        {
            return File.ReadAllLines(filePath);
        }

        public static char[][] GetMatrix(string filePath)
        {
            var lines = File.ReadAllLines(filePath);

            var matrix = new char[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                matrix[i] = lines[i].ToCharArray();
            }

            return matrix;
        }
    }
}
