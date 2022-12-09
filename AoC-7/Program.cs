
//using var reader = new StreamReader("input/ex.txt");
using var reader = new StreamReader("input/day7input.txt"); // 1325919

var content = reader.ReadToEnd();

var factory = new CommandFactory();
var commands = factory.CreateCommands(content);

var context = new Context();

foreach(var command in commands)
{
    command.Run(context);
}

var sum = context.Directories.Where(x => x.GetSize() <= 100000).Sum(x => x.GetSize());

Console.WriteLine(sum);