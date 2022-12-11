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

    public PlayEngine(int rounds, IEnumerable<Monkey> monkeys, Part part)
    {
        _rounds = rounds;
        _monkeys = monkeys.ToArray();
        SetRelaxCulculation(part);
    }

    private void SetRelaxCulculation(Part part)
    {
        if (part != Part.PartTwo)
            return;

        var lcm = FindLCM(_monkeys.Select(x => x.DivideNumber));

        foreach(var monkey in _monkeys)
        {
            monkey.CalculateWorryLevelAfterPlaying = x => x % lcm;
        }
    }

    public PlayResult Start()
    {
        Console.WriteLine("Play started");
        for (var i = 0; i < _rounds; i++)
        {
            Console.WriteLine($"Round - {i+1}");
            PlayRound();
        }

        var monkeyBusiness = CalculateMonkeyBusiness();

        return new PlayResult(monkeyBusiness);
    }

    private void PlayRound()
    {
        foreach (var monkey in _monkeys)
        {
            var items = monkey.Play();

            foreach (var item in items)
            {
                _monkeys.ElementAt(item.DestinationMonkeyIndex).Items.Enqueue(item.Item);
            }
        }
    }

    private long CalculateMonkeyBusiness()
    {
        var topMonkeys = _monkeys.OrderByDescending(x => x.ItemsInspected).Take(2);
        return topMonkeys.ElementAt(0).ItemsInspected * topMonkeys.ElementAt(1).ItemsInspected;
    }

    public static long FindLCM(IEnumerable<long> numbers)
    {
        var result = numbers.First();
        foreach (var number in numbers.Skip(1))
        {
            result = number * result / FindGCD(number, result);
        }

        return result;
    }

    public static long FindGCD(long a, long b)
    {
        if (b == 0)
            return a;

        return FindGCD(b, a % b);
    }
}
