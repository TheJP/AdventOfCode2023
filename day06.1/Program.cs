var input = File.ReadAllLines(Environment.GetCommandLineArgs()[1]);
var numbers = input.Select(line => line.Split(':')[1]
    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse)
    .ToArray()).ToArray();
var times = numbers[0];
var distances = numbers[1];

int result = 1;

for (int i = 0; i < times.Length; ++i)
{
    int wins = 0;
    for (int speed = 1; speed < times[i]; ++speed)
    {
        if ((times[i] - speed) * speed > distances[i]) ++wins;
    }
    result *= wins;
}

Console.WriteLine(result);
