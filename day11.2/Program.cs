var input = File.ReadLines(Environment.GetCommandLineArgs()[1]);
var map = input.Select(line => line.ToCharArray()).ToArray();

HashSet<int> expandRow = new();
for (int y = 0; y < map.Length; ++y)
{
    if (!map[y].Contains('#')) expandRow.Add(y);
}

HashSet<int> expandColumn = new();
for (int x = map[0].Length - 1; x >= 0; --x)
{
    bool empty = true;
    for (int y = 0; y < map.Length; ++y)
    {
        if (map[y][x] == '.') continue;
        empty = false;
        break;
    }

    if (empty) expandColumn.Add(x);
}

List<(int x, int y)> galaxies = new();
for (int y = 0; y < map.Length; ++y)
{
    for (int x = 0; x < map[y].Length; ++x)
    {
        if (map[y][x] == '#') galaxies.Add((x, y));
        // Console.Write(map[y][x]);
    }
    // Console.WriteLine();
}

long sum = 0;
for (int i = 0; i < galaxies.Count; ++i)
{
    for (int j = i + 1; j < galaxies.Count; ++j)
    {
        sum += Math.Abs(galaxies[i].x - galaxies[j].x) + Math.Abs(galaxies[i].y - galaxies[j].y);
        for (int x = Math.Min(galaxies[i].x, galaxies[j].x) + 1; x < Math.Max(galaxies[i].x, galaxies[j].x); ++x)
        {
            if (expandColumn.Contains(x)) sum += 1_000_000 - 1;
        }
        for (int y = Math.Min(galaxies[i].y, galaxies[j].y) + 1; y < Math.Max(galaxies[i].y, galaxies[j].y); ++y)
        {
            if (expandRow.Contains(y)) sum += 1_000_000 - 1;
        }
    }
}

Console.WriteLine(sum);
