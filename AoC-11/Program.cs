using MonkeyShenanigans;

var instructionsReader = new InstructionsReader();

//var monkeys = instructionsReader.GetMonkeys("input/ex1.txt");
var monkeys = instructionsReader.GetMonkeys("input/input1.txt"); // 78960, 14561971968

var playEngine = new PlayEngine(10000, monkeys, Part.PartTwo);
var result = playEngine.Start();

Console.WriteLine($"MonkeyBusiness: {result.MonkeyBusiness}");
Console.ReadLine();
