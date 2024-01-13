var input = File.ReadAllText(Environment.GetCommandLineArgs()[1]).Split(',');

int sum = 0;

foreach (var value in input)
{
    int hash = 0;
    foreach (var c in value)
    {
        hash += c;
        hash *= 17;
        hash %= 256;
    }

    sum += hash;
}

Console.WriteLine(sum);
