
public interface ICommand
{
    public void Run(Context context);
}

public class MoveCommand : ICommand
{
    private readonly string directoryName;

    public MoveCommand(string path)
    {
        directoryName = path;
    }

    public void Run(Context context)
    {
        if (directoryName == "/")
        {
            context.CurrentDirectory = context.Directories[0];
            return;
        }           

        if (directoryName == "..")
        {
            context.CurrentDirectory = context.CurrentDirectory.ParentDirectory;
            return;
        }

        context.CurrentDirectory = context.CurrentDirectory.Directories.First(x => x.Name == directoryName);
    }
}

public class ListCommand : ICommand
{
    private readonly List<Directory> _directories;
    private readonly List<File> _files;

    public ListCommand(List<Directory> directories, List<File> files)
    {
        _directories = directories;
        _files = files;
    }

    public void Run(Context context)
    {
        foreach (var directory in _directories)
        {
            directory.ParentDirectory = context.CurrentDirectory;
            context.CurrentDirectory.Directories.Add(directory);
            context.Directories.Add(directory);
        }

        foreach (var file in _files)
            context.CurrentDirectory.Files.Add(file);
    }
}
