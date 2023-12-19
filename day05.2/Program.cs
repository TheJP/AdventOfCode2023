var input = File.ReadAllLines(Environment.GetCommandLineArgs()[1]);

long[] seeds = input[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();

List<List<(long targetFrom, long sourceFrom, long length)>> maps = new();

for (int i = 3; i < input.Length; ++i)
{
    List<(long, long, long)> current = new();
    while (i < input.Length && input[i].Length > 0)
    {
        var numbers = input[i].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
        current.Sort((a, b) => a.Item1.CompareTo(b.Item1));
        current.Add((numbers[0], numbers[1], numbers[2]));
        ++i;
    }
    maps.Add(current);
    ++i;
}

long minLocation = long.MaxValue;

for (int i = 0; i < seeds.Length; i += 2)
{
    List<(long from, long length)> current = new()
    {
        (seeds[i], seeds[i + 1]),
    };

    foreach (var map in maps)
    {
        List<(long, long)> next = new();
        for (int s = 0; s < current.Count; ++s)
        {
            var source = current[s];
            bool found = false;
            foreach (var line in map)
            {
                if ((source.from >= line.sourceFrom && source.from <= line.sourceFrom + line.length - 1) ||
                    (source.from + source.length - 1 >= line.sourceFrom && source.from + source.length - 1 <= line.sourceFrom + line.length - 1) ||
                    (line.sourceFrom >= source.from && line.sourceFrom <= source.from + source.length - 1) ||
                    (line.sourceFrom + line.length - 1 >= source.from && line.sourceFrom + line.length - 1 <= source.from + source.length - 1))
                {
                    var left = source.from;
                    var length = source.length;

                    var leftLength = line.sourceFrom - source.from;
                    if (leftLength > 0)
                    {
                        current.Add((source.from, leftLength));
                        left = line.sourceFrom;
                        length -= leftLength;
                    }
                    var rightLength = (source.from + source.length) - (line.sourceFrom + line.length);
                    if (rightLength > 0)
                    {
                        current.Add((line.sourceFrom + line.length, rightLength));
                        length -= rightLength;
                    }
                    next.Add((line.targetFrom + (left - line.sourceFrom), length));

                    found = true;
                    break;
                }
            }

            if (!found)
            {
                next.Add(source);
            }
        }
        current = next;
    }

    minLocation = Math.Min(minLocation, current.Min(s => s.from));
    if (minLocation == 0) { break; }
}

Console.WriteLine(minLocation);
