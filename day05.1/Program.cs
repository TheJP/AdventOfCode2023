var input = File.ReadAllLines(Environment.GetCommandLineArgs()[1]);

long[] seeds = input[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();

List<List<(long targetFrom, long sourceFrom, long length)>> maps = new();

for (int i = 3; i < input.Length; ++i)
{
    List<(long, long, long)> current = new();
    while (i < input.Length && input[i].Length > 0)
    {
        var numbers = input[i].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
        current.Add((numbers[0], numbers[1], numbers[2]));
        ++i;
    }
    maps.Add(current);
    ++i;
}

long minLocation = long.MaxValue;

foreach (var seed in seeds)
{
    long current = seed;
    foreach (var map in maps)
    {
        foreach (var line in map)
        {
            if (current >= line.sourceFrom && current <= line.sourceFrom + line.length)
            {
                current = line.targetFrom + (current - line.sourceFrom);
                break;
            }
        }
    }

    minLocation = Math.Min(minLocation, current);
}

Console.WriteLine(minLocation);
