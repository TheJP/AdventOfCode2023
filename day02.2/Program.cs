var input = File.ReadLines(Environment.GetCommandLineArgs()[1]);

long sum = 0;
foreach (var line in input)
{
    var parts = line.Split(':');
    int gameId = int.Parse(parts[0][5..]);
    var sets = parts[1].Split(';');

    long red = 0;
    long green = 0;
    long blue = 0;

    foreach (var set in sets)
    {
        var colours = set.Split(',');
        foreach (var colour in colours)
        {
            var pair = colour.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);
            var count = int.Parse(pair[0]);
            var name = pair[1];

            switch (name)
            {
                case "red":
                    red = Math.Max(red, count);
                    break;
                case "green":
                    green = Math.Max(green, count);
                    break;
                case "blue":
                    blue = Math.Max(blue, count);
                    break;
            }
        }
    }

    sum += red * green * blue;
}

Console.WriteLine(sum);
