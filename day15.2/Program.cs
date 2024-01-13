var input = File.ReadAllText(Environment.GetCommandLineArgs()[1]).Split(',');

static int Hash(string value)
{
    int hash = 0;
    foreach (var c in value)
    {
        hash += c;
        hash *= 17;
        hash %= 256;
    }

    return hash;
}

var hashmap = new List<(string label, int value)>[255];
for (int i = 0; i < hashmap.Length; ++i) hashmap[i] = new();

#pragma warning disable CS8321 // Local function is declared but never used
void Print()
{
    for (int i = 0; i < hashmap!.Length; ++i)
    {
        if (hashmap[i].Count == 0) continue;
        Console.Write($"Box {i}: ");
        foreach (var (label, value) in hashmap[i])
        {
            Console.Write($"[{label} {value}] ");
        }
        Console.WriteLine();
    }
}
#pragma warning restore CS8321 // Local function is declared but never used

foreach (var line in input)
{
    if (line.Contains('='))
    {
        var parts = line.Split('=');
        var label = parts[0];
        var value = int.Parse(parts[1]);
        var hash = Hash(label);
        var index = hashmap[hash].FindIndex(p => p.label == label);
        if (index >= 0)
        {
            hashmap[hash][index] = (label, value);
        }
        else
        {
            hashmap[hash].Add((label, value));
        }
    }
    else
    {
        var label = line[..^1];
        var hash = Hash(label);
        var index = hashmap[hash].FindIndex(p => p.label == label);
        if (index >= 0)
        {
            hashmap[hash].RemoveAt(index);
        }
    }
}

int sum = 0;
for (int i = 0; i < hashmap.Length; ++i)
{
    for (int j = 0; j < hashmap[i].Count; ++j)
    {
        sum += (i + 1) * (j + 1) * hashmap[i][j].value;
    }
}

Console.WriteLine(sum);
