﻿var input = File.ReadLines(Environment.GetCommandLineArgs()[1]);

int sum = 0;
foreach (var line in input)
{
    var parts = line.Split(':');
    int gameId = int.Parse(parts[0][5..]);
    var sets = parts[1].Split(';');

    bool possible = true;

    foreach (var set in sets)
    {
        var colours = set.Split(',');
        foreach (var colour in colours)
        {
            var pair = colour.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);
            var count = int.Parse(pair[0]);
            var name = pair[1];
            // Console.WriteLine($"{count} -> {name}");

            switch (name)
            {
                case "red" when count > 12:
                    possible = false;
                    break;
                case "green" when count > 13:
                    possible = false;
                    break;
                case "blue" when count > 14:
                    possible = false;
                    break;
            }
        }
    }

    if (possible)
    {
        sum += gameId;
    }
}

Console.WriteLine(sum);
