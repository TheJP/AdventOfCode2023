var input = File.ReadLines(Environment.GetCommandLineArgs()[1]);
char[][] map = input.Select(line => line.ToCharArray()).ToArray();

void Print()
{
    for (int y = 0; y < map.Length; ++y)
    {
        for (int x = 0; x < map[y].Length; ++x) Console.Write(map[y][x]);
        Console.WriteLine();
    }
    Console.WriteLine();
}

void Move(Direction d)
{
    if (d == Direction.North || d == Direction.South)
    {
        int dy = d == Direction.North ? -1 : 1;
        for (int y = d == Direction.North ? 0 : (map.Length - 1); d == Direction.North ? y < map.Length : y >= 0; y -= dy)
        {
            for (int x = 0; x < map[y].Length; ++x)
            {
                if (map[y][x] != 'O') continue;
                for (int yO = y + dy; d == Direction.North ? yO >= 0 : yO < map.Length; yO += dy)
                {
                    if (map[yO][x] != '.') break;
                    map[yO][x] = 'O';
                    map[yO - dy][x] = '.';
                }
            }
        }
    }
    else
    {
        int dx = d == Direction.West ? -1 : 1;
        for (int x = d == Direction.West ? 0 : (map[0].Length - 1); d == Direction.West ? x < map[0].Length : x >= 0; x -= dx)
        {
            for (int y = 0; y < map.Length; ++y)
            {
                if (map[y][x] != 'O') continue;
                for (int xO = x + dx; d == Direction.West ? xO >= 0 : xO < map[y].Length; xO += dx)
                {
                    if (map[y][xO] != '.') break;
                    map[y][xO] = 'O';
                    map[y][xO - dx] = '.';
                }
            }
        }
    }
}

void Cycle()
{
    Move(Direction.North);
    Move(Direction.West);
    Move(Direction.South);
    Move(Direction.East);
}

bool Equals(char[][] other)
{
    for (int y = 0; y < map.Length; ++y)
    {
        for (int x = 0; x < map[y].Length; ++x)
        {
            if (map[y][x] != other[y][x]) return false;
        }
    }
    return true;
}

long cycles = 0;

List<char[][]> previous = new();
while (true)
{
    previous.Add(map.Select(line => line.ToArray()).ToArray());
    Cycle();
    ++cycles;

    int found = -1;
    for (int i = previous.Count - 1; i >= 0; --i)
    {
        if (Equals(previous[i]))
        {
            found = i;
            break;
        }
    }

    if (found >= 0)
    {
        Console.WriteLine($"Found {previous.Count}-{found} after {cycles}");
        long repeatLength = previous.Count - found;
        long remainingCycles = (1_000_000_000L - cycles) % repeatLength;
        for (long i = 0; i < remainingCycles; ++i) Cycle();
        break;
    }
}

int sum = 0;

for (int y = 0; y < map.Length; ++y)
{
    for (int x = 0; x < map[0].Length; ++x)

    {
        if (map[y][x] != 'O') continue;
        sum += map.Length - y;
    }
}

Print();
Console.WriteLine(sum);

enum Direction { North, South, East, West };
