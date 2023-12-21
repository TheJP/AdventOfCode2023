var input = File.ReadLines(Environment.GetCommandLineArgs()[1]);

var hands = input
    .Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries))
    .Select(line => (hand: line[0], bid: long.Parse(line[1]), counts: line[0].GroupBy(c => c).Select(g => g.Count()).OrderDescending().ToArray()))
    .ToList();

char[] cards = { 'A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2' };

hands.Sort((a, b) =>
{
    if (a.counts[0] != b.counts[0]) return a.counts[0].CompareTo(b.counts[0]);
    if (a.counts is [3, 2] && b.counts is not [3, 2]) return 1;
    if (b.counts is [3, 2] && a.counts is not [3, 2]) return -1;
    if (a.counts is [2, 2, ..] && b.counts is not [2, 2, ..]) return 1;
    if (b.counts is [2, 2, ..] && a.counts is not [2, 2, ..]) return -1;

    for (int i = 0; i < 5; ++i)
    {
        if (a.hand[i] == b.hand[i]) continue;
        var valueA = Array.IndexOf(cards, a.hand[i]);
        var valueB = Array.IndexOf(cards, b.hand[i]);
        return valueB.CompareTo(valueA);
    }

    return 0;
});

Console.WriteLine(hands.Select((hand, index) => hand.bid * (index + 1)).Sum());
