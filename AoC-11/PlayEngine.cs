namespace MonkeyShenanigans;

public record struct PlayResult(long MonkeyBusiness);

public enum Part
{
    PartOne,
    PartTwo,
}

public class PlayEngine
{
    private readonly int _rounds;
    private IEnumerable<Monkey> _monkeys;
    private Func<long, long> _calculateWorryLevelAfterPlaying = x => x / 3;

    public PlayEngine(int rounds, IEnumerable<Monkey> monkeys, Part part)
    {
        _rounds = rounds;
        _monkeys = monkeys;
        SetRelaxCulculation(part);
    }

    private void SetRelaxCulculation(Part part)
    {
        if (part != Part.PartTwo)
            return;

        var lcm = LCMHelper.FindLCM(_monkeys.Select(x => x.DivideNumber));
        _calculateWorryLevelAfterPlaying = x => x % lcm;
    }

    public PlayResult Start()
    {
        for (var i = 0; i < _rounds; i++)
            PlayRound();

        var monkeyBusiness = CalculateMonkeyBusiness();
        return new PlayResult(monkeyBusiness);
    }

    private void PlayRound()
    {
        foreach (var monkey in _monkeys)
        {
            var items2 = monkey.Play();
            items2 = items2.Select(x => _calculateWorryLevelAfterPlaying(x));

            foreach (var item in items2)
            {
                var index = monkey.GetItemNextDestination(item);
                _monkeys.ElementAt(index).Items.Enqueue(item);
            }
        }
    }

    private long CalculateMonkeyBusiness()
    {
        var topMonkeys = _monkeys.OrderByDescending(x => x.ItemsInspected).Take(2);
        return topMonkeys.ElementAt(0).ItemsInspected * topMonkeys.ElementAt(1).ItemsInspected;
    }
}