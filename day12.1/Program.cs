var input = File.ReadLines(Environment.GetCommandLineArgs()[1]);
var maps = input.Select(line => line.Split(' ')[0].ToCharArray()).ToArray();
var searchMap = maps.Select(line => new string(line).Replace('?', '#')).ToArray();
var numbers = input.Select(line => line.Split(' ')[1].Split(',').Select(int.Parse).ToArray()).ToArray();

int count(int i, int leftMap, int leftNum)
{
    // Console.WriteLine($"{i} {leftMap} {leftNum}");
    if (leftNum >= numbers[i].Length)
    {
        bool finished = true;
        for (int j = leftMap; finished && j < maps[i].Length; ++j)
        {
            if (maps[i][j] == '#') finished = false;
        }
        return finished ? 1 : 0;
    }
    if (leftMap >= maps[i].Length) return 0;

    var num = numbers[i][leftNum];

    var next = searchMap[i].IndexOf('#', leftMap);
    if (next < 0) return 0;

    int sum = 0;
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

        // Console.WriteLine($"{leftNum}: {left}-{left + num - 1}");

        sum += count(i, left + num + 1, leftNum + 1);
    }

    return sum;
}

int sum = 0;
for (int i = 0; i < numbers.Length; ++i)
{
    // Console.WriteLine(count(i, 0, 0));
    sum += count(i, 0, 0);
    // Console.WriteLine(sum);
    // break;
}

Console.WriteLine(sum);
