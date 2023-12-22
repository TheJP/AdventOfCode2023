var input = File.ReadLines(Environment.GetCommandLineArgs()[1]);

var instructions = input.First();
var map = input.Skip(2)
    .Select(line =>
    {
        var parts = line.Split('=');
        var origin = parts[0].Trim();
        parts = parts[1].Trim()[1..^1].Split(',');
        var left = parts[0].Trim();
        var right = parts[1].Trim();
        return (origin, left, right);
    })
    .ToDictionary(t => t.origin, t => (t.left, t.right));

int count = 0;
var current = "AAA";
while (current != "ZZZ")
{
    foreach (var instruction in instructions)
    {
        current = instruction switch
        {
            'L' => map[current].left,
            'R' => map[current].right,
            _ => throw new InvalidOperationException(),
        };
    }
    count += instructions.Length;
}

Console.WriteLine(count);
