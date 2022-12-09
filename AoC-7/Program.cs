// See https://aka.ms/new-console-template for more information
// 1325919

//using var reader = new StreamReader("ex.txt");
using var reader = new StreamReader("input/day7input.txt");

var content = reader.ReadToEnd();

var factory = new CommandFactory();
var commands = factory.CreateCommands(content);

var context = new Context();

foreach(var command in commands)
{
    command.Run(context);
}

var dir = context.Directories.Where(x => x.GetSize() <= 100000);
var sum = dir.Sum(x => x.GetSize());

Console.WriteLine(sum);