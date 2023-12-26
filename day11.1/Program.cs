var input = File.ReadLines(Environment.GetCommandLineArgs()[1]);
var map = input.SelectMany(line => line.Contains('#') ? new[] { line.ToList() } : new[] { line.ToList(), line.ToList() }).ToArray();

for (int x = map[0].Count - 1; x >= 0; --x)
{
    bool empty = true;
    for (int y = 0; y < map.Length; ++y)
    {
        if (map[y][x] == '.') continue;
        empty = false;
        break;
    }

    if (empty)
    {
        for (int y = 0; y < map.Length; ++y)
        {
            map[y].Insert(x, '.');
        }
    }
}

List<(int x, int y)> galaxies = new();
for (int y = 0; y < map.Length; ++y)
{
    for (int x = 0; x < map[y].Count; ++x)
    {
        if (map[y][x] == '#') galaxies.Add((x, y));
        // Console.Write(map[y][x]);
    }
    // Console.WriteLine();
}

int sum = 0;
for (int i = 0; i < galaxies.Count; ++i)
{
    for (int j = i + 1; j < galaxies.Count; ++j)
    {
        sum += Math.Abs(galaxies[i].x - galaxies[j].x) + Math.Abs(galaxies[i].y - galaxies[j].y);
    }
}

Console.WriteLine(sum);
