var input = File.ReadLines(Environment.GetCommandLineArgs()[1]);

int sum = 0;

List<char[]> map = new();
foreach (var line in input)
{
    if (line.Trim().Length > 0)
    {
        map.Add(line.ToCharArray());
        continue;
    }

    bool isMirror = false;
    for (int x0 = 0; !isMirror && x0 < map[0].Length - 1; ++x0)
    {
        isMirror = true;
        for (int y = 0; isMirror && y < map.Count; ++y)
        {
            for (int x1 = x0, x2 = x0 + 1; x1 >= 0 && x2 < map[y].Length; --x1, ++x2)
            {
                if (map[y][x1] != map[y][x2])
                {
                    isMirror = false;
                    break;
                }
            }
        }

        if (isMirror) sum += x0 + 1;
        // if (isMirror) Console.WriteLine($"x {x0}");
    }

    if (isMirror)
    {
        map.Clear();
        continue;
    }

    for (int y0 = 0; !isMirror && y0 < map.Count - 1; ++y0)
    {
        isMirror = true;
        for (int y1 = y0, y2 = y1 + 1; isMirror && y1 >= 0 && y2 < map.Count; --y1, ++y2)
        {
            for (int x = 0; x < map[0].Length; ++x)
            {
                if (map[y1][x] != map[y2][x])
                {
                    isMirror = false;
                    break;
                }
            }
        }

        if (isMirror) sum += 100 * (y0 + 1);
        // if (isMirror) Console.WriteLine($"y {y0}");
    }

    if (!isMirror) Console.WriteLine("No Mirror Found!");

    map.Clear();
}

Console.WriteLine(sum);
