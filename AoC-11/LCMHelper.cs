namespace MonkeyShenanigans;

public static class LCMHelper
{
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