public class CommandFactory
{
    public IEnumerable<ICommand> CreateCommands(string input)
    {
        var commands = new List<ICommand>();
        var splitInput = input.Split("$");
        foreach (var command in splitInput)
        {
            if (string.IsNullOrEmpty(command))
                continue;

            var output = command.Split("\r\n").Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x));

            var c = CreateCommand(output);
            commands.Add(c);
        }

        return commands;
    }

    private ICommand CreateCommand(IEnumerable<string> input)
    {
        var args = input.First().Split(" ");

        ICommand command = args[0] switch
        {
            "cd" => new MoveCommand(args[1]),
            "ls" => CreateListCommand(input.Skip(1)),
            _ => throw new NotSupportedException(""),
        };

        return command;
    }

    private ListCommand CreateListCommand(IEnumerable<string> input)
    {
        var directories = new List<Directory>();
        var files = new List<File>();

        foreach(var row in input)
        {
            var info = row.Split(" ");
            if(info[0] == "dir")
            {
                directories.Add(new Directory() { Name = info[1] });
                continue;
            }

            files.Add(new File() { Name = info[1], Size = int.Parse(info[0]) });
        }

        return new ListCommand(directories, files);
    }
}