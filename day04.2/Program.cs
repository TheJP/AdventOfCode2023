var input = File.ReadAllLines(Environment.GetCommandLineArgs()[1]);
int sum = 0;

int[] count = new int[input.Length];
Array.Fill(count, 1);

int i = 0;
foreach (var line in input)
{
    sum += count[i];

    var numbers = line.Split(":")[1].Split("|").Select(part => part
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse))
        .ToArray();

    var winning = numbers[0].ToHashSet();
    var wins = numbers[1].Count(winning.Contains);

    for (int j = 0; j < wins; ++j)
    {
        count[i + j + 1] += count[i];
    }

    ++i;
}

Console.WriteLine(sum);
