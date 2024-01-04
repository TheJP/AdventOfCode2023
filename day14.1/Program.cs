var input = File.ReadAllLines(Environment.GetCommandLineArgs()[1]);

int sum = 0;

for (int x = 0; x < input[0].Length; ++x)
{
    int rocks = input.Length;
    for (int y = 0; y < input.Length; ++y)
    {
        if (input[y][x] == '.') continue;
        if (input[y][x] == '#')
        {
            rocks = input.Length - y - 1;
            continue;
        }
        sum += rocks;
        --rocks;
    }
}

Console.WriteLine(sum);
