var input = File.ReadLines(Environment.GetCommandLineArgs()[1]);
var numbers = input.Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).Append(0).ToArray()).ToArray();

int sum = 0;

foreach (var row in numbers)
{
    List<int[]> diffs = new() { row };
    int i = 0;
    while (true)
    {
        diffs.Add(new int[diffs[i].Length - 1]);
        bool allZero = true;
        for (int x = 0; x + 1 < diffs[i].Length - 1; ++x)
        {
            diffs[i + 1][x] = diffs[i][x + 1] - diffs[i][x];
            if (diffs[i + 1][x] != 0) allZero = false;
        }
        if (allZero) break;
        ++i;
    }
    for (; i >= 0; --i)
    {
        diffs[i][^1] = diffs[i + 1][^1] + diffs[i][^2];
    }
    sum += diffs[0][^1];
}

Console.WriteLine(sum);
