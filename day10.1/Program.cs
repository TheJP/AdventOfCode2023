var input = File.ReadLines(Environment.GetCommandLineArgs()[1]);
var topBot = new string('.', input.First().Length + 2);
var map = input.Select(line => $".{line}.").Prepend(topBot).Append(topBot).Select(line => line.ToCharArray()).ToArray();

(int x, int y) start = map.SelectMany((row, y) => row.Select((c, x) => ((x, y), c))).Single(t => t.c == 'S').Item1;

int highest = 0;
int[,] distance = new int[map.Length, map[0].Length];
Queue<(int x, int y, int cost)> queue = new();
queue.Enqueue((start.x, start.y, 0));
Dictionary<char, (int x, int y)[]> permutations = new() {
    { '|', new[] { (0, 1), (0, -1) } },
    { '-', new[] { (1, 0), (-1, 0) } },
    { 'L', new[] { (1, 0), (0, -1) } },
    { 'J', new[] { (-1, 0), (0, -1) } },
    { '7', new[] { (-1, 0), (0, 1) } },
    { 'F', new[] { (1, 0), (0, 1) } },
    { 'S', new[] { (0, 1), (0, -1), (1, 0), (-1, 0) }},
};

while (queue.Count > 0)
{
    var (x, y, cost) = queue.Dequeue();
    foreach (var permutation in permutations[map[y][x]])
    {
        var next = (x: x + permutation.x, y: y + permutation.y, cost: cost + 1);
        if (map[next.y][next.x] == '.' ||
            !permutations.ContainsKey(map[next.y][next.x]) ||
            !permutations[map[next.y][next.x]].Contains((-permutation.x, -permutation.y)))
        {
            continue;
        }
        if (distance[next.y, next.x] == 0)
        {
            distance[next.y, next.x] = next.cost;
            highest = Math.Max(highest, next.cost);
            queue.Enqueue(next);
        }
    }
}


Console.WriteLine(highest);
