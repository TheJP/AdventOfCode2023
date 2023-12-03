var input = File.ReadAllLines(Environment.GetCommandLineArgs()[1]);
var topBot = new string('.', input[0].Length + 2);
input = (new[] { topBot })
    .Concat(input.Select(row => $".{row}."))
    .Concat(new[] { topBot })
    .ToArray();

bool[,] visited = new bool[input.Length, input[0].Length];

int sum = 0;
for (int y = 1; y < input.Length - 1; ++y)
{
    for (int x = 1; x < input[y].Length - 1; ++x)
    {
        if (input[y][x] == '.') continue;
        if (char.IsDigit(input[y][x])) continue;

        for (int dy = -1; dy <= 1; ++dy)
        {
            for (int dx = -1; dx <= 1; ++dx)
            {
                var (digitY, digitX) = (y + dy, x + dx);

                if (visited[digitY, digitX]) continue;
                if (!char.IsDigit(input[digitY][digitX])) continue;

                while (char.IsDigit(input[digitY][digitX - 1])) --digitX;
                int number = 0;
                while (char.IsDigit(input[digitY][digitX]))
                {
                    visited[digitY, digitX] = true;
                    number = (number * 10) + (input[digitY][digitX] - '0');
                    ++digitX;
                }
                sum += number;
            }
        }
    }
}

Console.WriteLine(sum);
