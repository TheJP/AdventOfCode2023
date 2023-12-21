var input = File.ReadLines(Environment.GetCommandLineArgs()[1]);

var hands = input
    .Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries))
    .Select(line => (hand: line[0], bid: long.Parse(line[1]), jokers: line[0].Count(c => c == 'J'), counts: line[0].Where(c => c != 'J').GroupBy(c => c).Select(g => g.Count()).OrderDescending().ToArray()))
    .ToList();

char[] cards = { 'A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2', 'J' };

hands.Sort((a, b) =>
{
    int result;
    int aSum = (a.counts.Length > 0 ? a.counts[0] : 0) + a.jokers;
    int bSum = (b.counts.Length > 0 ? b.counts[0] : 0) + b.jokers;
    if (aSum != bSum) result = aSum.CompareTo(bSum);
    else if (a.counts is [2 or 3, 2] && b.counts is [2 or 3, 2]) result = 0;
    else if (a.counts is [2 or 3, 2] && b.counts is not [2 or 3, 2]) result = 1;
    else if (b.counts is [2 or 3, 2] && a.counts is not [2 or 3, 2]) result = -1;
    else if (a.counts is [2, 2, ..] && b.counts is [2, 2, ..]) result = 0;
    else if (a.counts is [2, 2, ..] && b.counts is not [2, 2, ..]) result = 1;
    else if (b.counts is [2, 2, ..] && a.counts is not [2, 2, ..]) result = -1;
    else result = 0;

    if (result != 0) return result;

    for (int i = 0; i < 5; ++i)
    {
        if (a.hand[i] == b.hand[i]) continue;
        var valueA = Array.IndexOf(cards, a.hand[i]);
        var valueB = Array.IndexOf(cards, b.hand[i]);
        return valueB.CompareTo(valueA);
    }

    return 0;
});

foreach (var hand in hands)
{
    Console.WriteLine(hand.hand);
}

Console.WriteLine(hands.Select((hand, index) => hand.bid * (index + 1)).Sum());
