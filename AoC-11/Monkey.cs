namespace MonkeyShenanigans;

public class Monkey
{
    public Func<long, long> CalculateWorryLevelWhilePlaying { get; set; } = x => throw new NotImplementedException();
    public Queue<long> Items { get; private set; } = new Queue<long>();
    public long DivideNumber { get; set; }
    public int HappyThrowIndex { get; set; }
    public int SadThrowIndex { get; set; }
    public long ItemsInspected { get; private set; }

    public IEnumerable<long> Play()
    {
        if (!Items.Any())
            return Enumerable.Empty<long>();

        var throwItems = new List<long>();
        while (Items.Count > 0)
        {
            ItemsInspected++;
            var item = Items.Dequeue();

            var worryLevel = CalculateWorryLevelWhilePlaying(item);
            throwItems.Add(worryLevel);
        }

        return throwItems;
    }

    public int GetItemNextDestination(long item)
    {
        var isDivisible = item % DivideNumber;

        var destination = isDivisible == 0 ? HappyThrowIndex : SadThrowIndex;
        return destination;
    }
}
