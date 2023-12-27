var input = File.ReadLines(Environment.GetCommandLineArgs()[1]);
var maps = input.Select(line => string.Join('?', Enumerable.Repeat(line.Split(' ')[0], 5)).ToCharArray()).ToArray();
var searchMap = maps.Select(line => new string(line).Replace('?', '#')).ToArray();
var numbers = input.Select(line => Enumerable.Repeat(line.Split(' ')[1].Split(',').Select(int.Parse), 5).SelectMany(x => x).ToArray()).ToArray();

long[][] mem = Array.Empty<long[]>();

long count(int i, int leftMap, int leftNum)
{
    if (leftMap < mem.Length && mem[leftMap][leftNum] >= 0)
    {
        return mem[leftMap][leftNum];
    }

    if (leftNum >= numbers[i].Length)
    {
        bool finished = true;
        for (int j = leftMap; finished && j < maps[i].Length; ++j)
        {
            if (maps[i][j] == '#') finished = false;
        }
        var result = finished ? 1 : 0;
        if (leftMap < mem.Length) mem[leftMap][leftNum] = result;
        return result;
    }
    if (leftMap >= maps[i].Length)
    {
        if (leftMap < mem.Length) mem[leftMap][leftNum] = 0;
        return 0;
    }

    var num = numbers[i][leftNum];

    var next = searchMap[i].IndexOf('#', leftMap);
    if (next < 0) return 0;

    long sum = 0;
    for (int left = next; left + num - 1 < maps[i].Length; ++left)
    {
        bool canFit = true;
        for (int j = next; canFit && j < left; ++j)
        {
            if (maps[i][j] == '#') canFit = false;
        }
        if (!canFit) break;
        for (int j = 0; canFit && j < num; ++j)
        {
            if (searchMap[i][left + j] != '#') canFit = false;
        }
        if (!canFit || (left + num < maps[i].Length && maps[i][left + num] == '#'))
        {
            continue;
        }

        sum += count(i, left + num + 1, leftNum + 1);
    }

    mem[leftMap][leftNum] = sum;
    return sum;
}

long sum = 0;
for (int i = 0; i < numbers.Length; ++i)
{
    mem = new long[maps[i].Length + 1][];
    for (int j = 0; j < mem.Length; ++j)
    {
        mem[j] = new long[numbers[i].Length + 1];
        Array.Fill(mem[j], -1);
    }

    sum += count(i, 0, 0);
}

Console.WriteLine(sum);
