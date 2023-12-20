var input = File.ReadAllLines(Environment.GetCommandLineArgs()[1]);
var numbers = input.Select(line => long.Parse(line.Split(':')[1].Replace(" ", ""))).ToArray();
var times = numbers[0];
var distances = numbers[1];

int wins = 0;
for (long speed = 1; speed < times; ++speed)
{
    if ((times - speed) * speed > distances) ++wins;
}

Console.WriteLine(wins);
