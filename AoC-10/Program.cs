

var reader = new StreamReader("input/input.txt"); // 14920

var input = reader.ReadToEnd();

var rows = input.Split("\r\n");

var commands = rows.Select(x =>
{
    var args = x.Split(' ');
    if (args[0] == "addx")
        return new Command { CycleTime = 2, Value = int.Parse(args[1]) };

    return new Command { CycleTime = 1 };
}).ToList();


var currentCycle = 0;
foreach(var command in commands)
{
    currentCycle += command.CycleTime;
    command.NewValueAtCycle = currentCycle + 1;
}

var cyclesToFind = new int[] { 20, 60, 100, 140, 180, 220 };

var totalSum = 0;
foreach(var cycleToFind in cyclesToFind)
{
    var commandsInCycle = commands.Where(x => x.NewValueAtCycle <= cycleToFind);
    var currentValue = commandsInCycle.Sum(x => x.Value) + 1;
    var signalStrength = currentValue * cycleToFind;    
    totalSum += signalStrength;
}

Console.WriteLine(totalSum);


public class Command
{
    public int CycleTime { get; set; }
    public int Value { get; set; }
    public int NewValueAtCycle { get; set; }
}