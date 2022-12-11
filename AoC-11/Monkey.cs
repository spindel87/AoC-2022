namespace MonkeyShenanigans;

public record struct ThrownItem(int DestinationMonkeyIndex, long Item);

public class Monkey
{
    public Func<long, long> CalculateWorryLevelWhilePlaying { get; set; } = x => throw new NotImplementedException();
    public Func<long, long> CalculateWorryLevelAfterPlaying { get; set; } = x => throw new NotImplementedException();

    public Queue<long> Items { get; private set; } = new Queue<long>();
    public long DivideNumber { get; set; }
    public int HappyThrowIndex { get; set; }
    public int SadThrowIndex { get; set; }
    public long ItemsInspected { get; private set; }    

    public IEnumerable<ThrownItem> Play()
    {
        if(!Items.Any())
            return Enumerable.Empty<ThrownItem>();

        var throwItems = new List<ThrownItem>();
        while (Items.Count > 0)
        {
            var item = PlayWithItem();
            throwItems.Add(item);
        }

        return throwItems;
    }

    private ThrownItem PlayWithItem()
    {
        ItemsInspected++;
        var item = Items.Dequeue();

        var worryLevel = CalculateWorryLevelWhilePlaying(item);

        worryLevel = CalculateWorryLevelAfterPlaying(worryLevel);

        var destination = GetItemNextDestination(worryLevel);

        return new ThrownItem(destination, worryLevel);
    }

    private int GetItemNextDestination(long worryLevel)
    {
        var isDivisible = worryLevel % DivideNumber;

        var destination = isDivisible == 0 ? HappyThrowIndex : SadThrowIndex;
        return destination;
    }
}
