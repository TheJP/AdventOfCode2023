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

var currents = map.Keys.Where(k => k.EndsWith('A')).ToArray();

// Bruteforce:
// int count = 0;
// while (currents.Any(k => !k.EndsWith('Z')))
// {
//     foreach (var instruction in instructions)
//     {
//         for (int i = 0; i < currents.Length; ++i)
//         {
//             currents[i] = instruction switch
//             {
//                 'L' => map[currents[i]].left,
//                 'R' => map[currents[i]].right,
//                 _ => throw new InvalidOperationException(),
//             };
//         }
//     }
//     count += instructions.Length;
// }

// https://www.geeksforgeeks.org/lcm-of-given-array-elements/
static long GreatestCommonDivisor(long a, long b)
{
    while (b != 0) (a, b) = (b, a % b);
    return a;
}

static long LeastCommonMultiple(IList<int> array)
{
    long result = array[0];

    for (int i = 1; i < array.Count; ++i)
    {
        result = (array[i] * result) / GreatestCommonDivisor(array[i], result);
    }

    return result;
}

var rounds = new int[currents.Length];
for (int i = 0; i < currents.Length; ++i)
{
    rounds[i] = 0;
    var current = currents[i];
    while (!current.EndsWith('Z'))
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
        rounds[i] += instructions.Length;
    }
}

// int result = rounds[0];
// foreach (var round in rounds.Skip(1)) result = findlcm(result * round, instructions.Length);
long result = LeastCommonMultiple(rounds);

Console.WriteLine(result);
