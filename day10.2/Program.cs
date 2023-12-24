var input = File.ReadLines(Environment.GetCommandLineArgs()[1]);
var topBot = new string('.', input.First().Length + 2);
var map = input.Select(line => $".{line}.").Prepend(topBot).Append(topBot).Select(line => line.ToCharArray()).ToArray();

(int x, int y) start = map.SelectMany((row, y) => row.Select((c, x) => ((x, y), c))).Single(t => t.c == 'S').Item1;

bool[,] isLoop = new bool[map.Length, map[0].Length];
isLoop[start.y, start.x] = true;
Queue<(int x, int y)> queue = new();
queue.Enqueue((start.x, start.y));
Dictionary<char, (int x, int y)[]> permutations = new() {
    { '|', new[] { (0, 1), (0, -1) } },
    { '-', new[] { (1, 0), (-1, 0) } },
    { 'L', new[] { (1, 0), (0, -1) } },
    { 'J', new[] { (-1, 0), (0, -1) } },
    { '7', new[] { (-1, 0), (0, 1) } },
    { 'F', new[] { (1, 0), (0, 1) } },
    { 'S', new[] { (0, 1), (0, -1), (1, 0), (-1, 0) } },
};

while (queue.Count > 0)
{
    var (x, y) = queue.Dequeue();
    foreach (var permutation in permutations[map[y][x]])
    {
        var next = (x: x + permutation.x, y: y + permutation.y);
        if (map[next.y][next.x] == '.' ||
            !permutations.ContainsKey(map[next.y][next.x]) ||
            !permutations[map[next.y][next.x]].Contains((-permutation.x, -permutation.y)))
        {
            continue;
        }
        if (!isLoop[next.y, next.x])
        {
            isLoop[next.y, next.x] = true;
            queue.Enqueue(next);
        }
    }
}

char[][] map2 = new char[map.Length * 2][];
for (int y = 0; y < map2.Length; ++y) map2[y] = new char[map[0].Length * 2];
for (int y = 0; y < map2.Length; ++y)
{
    for (int x = 0; x < map2[y].Length; ++x)
    {
        if (x % 2 == 0 && y % 2 == 0) map2[y][x] = map[y / 2][x / 2];
        else if (x % 2 == 0 && isLoop[(y - 1) / 2, x / 2] && permutations[map2[y - 1][x]].Contains((0, 1)))
        {
            map2[y][x] = '|';
        }
        else if (y % 2 == 0 && isLoop[y / 2, (x - 1) / 2] && permutations[map2[y][x - 1]].Contains((1, 0)))
        {
            map2[y][x] = '-';
        }
        else map2[y][x] = ' ';
    }
}


bool[,] isOutside = new bool[map2.Length, map2[0].Length];
queue.Enqueue((0, 0));
while (queue.Count > 0)
{
    var (x, y) = queue.Dequeue();
    foreach (var (dx, dy) in permutations['S'])
    {
        if (y + dy < 0 || y + dy >= map2.Length || x + dx < 0 || x + dx >= map2[y + dy].Length) continue;
        if (map2[y + dy][x + dx] != ' ' && isLoop[(y + dy) / 2, (x + dx) / 2]) continue;
        if (isOutside[y + dy, x + dx]) continue;
        isOutside[y + dy, x + dx] = true;
        queue.Enqueue((x + dx, y + dy));
    }
}

int count = 0;
for (int y = 0; y < map2.Length; ++y)
{
    for (int x = 0; x < map2[y].Length; ++x)
    {
        Console.Write(isOutside[y, x] ? '#' : map2[y][x]);
        if (!isOutside[y, x] && map2[y][x] != ' ' && !isLoop[y / 2, x / 2]) ++count;
    }
    Console.WriteLine();
}

Console.WriteLine(count);
