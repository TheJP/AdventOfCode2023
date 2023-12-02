var input = File.ReadLines(Environment.GetCommandLineArgs()[1]);

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
            var stuff = colour.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);
            var count = int.Parse(stuff[0]);
            var name = stuff[1];
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
// only 12 red cubes, 13 green cubes, and 14 blue cubes
// Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green