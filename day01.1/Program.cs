var input = File.ReadLines(Environment.GetCommandLineArgs()[1]);
int sum = 0;
foreach (var line in input)
{
    sum += int.Parse($"{line.First(char.IsDigit)}{line.Last(char.IsDigit)}");
}

Console.WriteLine(sum);
