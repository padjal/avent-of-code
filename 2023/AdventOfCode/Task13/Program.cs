using System.Diagnostics;

const string INPUT_FILE = "../../../input.txt";

var input = ParseInput();

PartOne();
PartTwo();

List<List<string>> ParseInput()
{
    var patterns = new List<List<string>>();

    var fileContent = File.ReadAllLines(INPUT_FILE);

    var pattern = new List<string>();

    foreach (var line in fileContent)
    {
        if (line == string.Empty)
        {
            // End of pattern

            patterns.Add(pattern);

            pattern = new List<string>();

            continue;
        }

        pattern.Add(line);
    }

    //Add last pattern
    patterns.Add(pattern);

    return patterns;
}

void PartOne()
{
    var timer = new Stopwatch();
    timer.Start();

    var sum = 0;

    foreach (var pattern in input)
    {
        // Check for horizontal reflection
        sum += FindHorizontalReflection(pattern);

        // Check for vertical reflection
        sum += FindVerticalReflection(pattern);
    }

    Console.WriteLine($"Sum: {sum}");

    timer.Stop();

    Console.WriteLine($"Part 1 completed in {timer.Elapsed.TotalMilliseconds} milliseconds.");
}

void PartTwo()
{

}

int FindVerticalReflection(List<string> pattern)
{
    int sum = 0;

    var varticalSymmetry = true;

    // For each different line of reflection (vertical)
    for(int i = 0; i < pattern[0].Count() - 1; i++)
    {
        var isReflection = true;

        // Check all rows, isReflection = false if not found everywhere
        foreach(var row in pattern)
        {
            if(!HasVerticalSymmetry(row, i))
            {
                isReflection = false;
                break;
            }
        }

        if (isReflection)
        {
            //Reflection found at i.
            sum += i + 1;
        }
    }    

    return sum;
}

int FindHorizontalReflection(List<string> pattern)
{
    // Transpose
    var rows = pattern.Count();
    var columns = pattern[0].Count();

    var initialMatrix = new char[rows][];

    for(int i = 0; i < pattern.Count; i++)
    {
        initialMatrix[i] = pattern[i].ToArray();
    }

    var transposedMatrix = new char[columns][];

    for (int i = 0; i < columns; i++)
    {
        transposedMatrix[i] = new char[rows];

        for (int j = 0; j< rows; j++)
        {
            transposedMatrix[i][j] = initialMatrix[j][i];
        }
    }

    pattern = new List<string>();

    for (int i = 0; i < transposedMatrix.Length; i++)
    {
        pattern.Add(new string(transposedMatrix[i]));
    }

    foreach(var line in pattern)
    {
        Console.WriteLine(line);
    }

    int sum = 0;

    var varticalSymmetry = true;

    // For each different line of reflection (vertical)
    for(int i = 0; i < pattern[0].Count() - 1; i++)
    {
        var isReflection = true;

        // Check all rows, isReflection = false if not found everywhere
        foreach(var row in pattern)
        {
            if(!HasVerticalSymmetry(row, i))
            {
                isReflection = false;
                break;
            }
        }

        if (isReflection)
        {
            //Reflection found at i.
            sum += (i + 1)*100;
        }
    }    

    return sum;
}

bool HasVerticalSymmetry(string text, int index)
{
    var leftIndex = index;
    var rightIndex = index + 1;

    while(leftIndex >= 0 && rightIndex < text.Length)
    {
        if (text[leftIndex] != text[rightIndex])
        {
            return false;
        }

        leftIndex--;
        rightIndex++;
    }

    return true;
}