var input = File.ReadLines(Environment.GetCommandLineArgs()[1]);
int sum = 0;
foreach (var line in input)
{
    var numbers = line.Split(":")[1].Split("|").Select(part => part
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse))
        .ToArray();

    var winning = numbers[0].ToHashSet();
    var wins = numbers[1].Count(winning.Contains);

    if (wins > 0) sum += 1 << (wins - 1);
}

Console.WriteLine(sum);
