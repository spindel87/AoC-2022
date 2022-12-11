using System.Text.RegularExpressions;

namespace MonkeyShenanigans;
public class InstructionsReader
{
    const string pattern = @"Monkey (?'index'\d):\n  Starting items: (?'items'(\d+(, )*)+)\n  Operation: new = old (?'operator'.).(?'operationValue'\w+)\n  Test: divisible by (?'divide'\d+)\n    If true: throw to monkey (?'happyIndex'\d+)\n    If false: throw to monkey (?'sadIndex'\d+)";

    public IEnumerable<Monkey> GetMonkeys(string inputFile)
    {
        var content = File.ReadAllText(inputFile);

        var regex = new Regex(pattern);

        var matches = regex.Matches(content.Replace("\r", ""));
        var count = matches.Count();

        var monkeys = new List<Monkey>();
        foreach (Match match in matches)
        {
            var index = int.Parse(match.Groups["index"].Value);
            var itemsString = match.Groups["items"].Value;
            var worryLevelOperator = match.Groups["operator"].Value;
            long.TryParse(match.Groups["operationValue"].Value, out var operationValue);
            var divide = long.Parse(match.Groups["divide"].Value);
            var happyIndex = int.Parse(match.Groups["happyIndex"].Value);
            var sadIndex = int.Parse(match.Groups["sadIndex"].Value);

            var items = itemsString.Split(',').Select(x => long.Parse(x.Trim()));

            var monkey = new Monkey
            {
                DivideNumber = divide,
                HappyThrowIndex = happyIndex,
                SadThrowIndex = sadIndex,
                CalculateWorryLevelWhilePlaying = ToCalculateWorryLevel(worryLevelOperator, operationValue),
                CalculateWorryLevelAfterPlaying = x => x / 3
            };

            foreach (var item in items)
                monkey.Items.Enqueue(item);

            monkeys.Add(monkey);
        }

        return monkeys;
    }

    private static Func<long, long> ToCalculateWorryLevel(string operatorChar, long operationValue) => operatorChar switch
    {
        "*" => x => x * (operationValue == 0 ? x : operationValue),
        "+" => x => x + operationValue,
        _ => throw new NotSupportedException(),
    };
}