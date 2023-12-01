var input = File.ReadLines(Environment.GetCommandLineArgs()[1]);

(string, int)[] wordToDigit = {
    ("one", 1),
    ("two", 2),
    ("three", 3),
    ("four", 4),
    ("five", 5),
    ("six", 6),
    ("seven", 7),
    ("eight", 8),
    ("nine", 9),
};

int sum = 0;
foreach (var line in input)
{
    int minIndex = int.MaxValue;
    int minDigit = -1;
    int maxIndex = -1;
    int maxDigit = -1;
    foreach (var (word, digit) in wordToDigit)
    {
        int index = line.IndexOf(word);
        if (index < 0) continue;

        if (index < minIndex)
        {
            minIndex = index;
            minDigit = digit;
        }

        index = line.LastIndexOf(word);
        if (index > maxIndex)
        {
            maxIndex = index;
            maxDigit = digit;
        }
    }

    var digits = line.Select((c, i) => (char.IsDigit(c), i, c));
    var (hasMin, minI, minD) = digits.FirstOrDefault(t => t.Item1);
    if (hasMin && minI < minIndex) minDigit = int.Parse(minD.ToString());
    var (hasMax, maxI, maxD) = digits.LastOrDefault(t => t.Item1);
    if (hasMax && maxI > maxIndex) maxDigit = int.Parse(maxD.ToString());

    sum += int.Parse($"{minDigit}{maxDigit}");
}

Console.WriteLine(sum);
