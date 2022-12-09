public class Context
{
    public Context()
    {
        var dir = new Directory { Name = "/" };
        CurrentDirectory = dir;
        Directories.Add(dir);
    }

    public Directory CurrentDirectory { get; set; }

    public IList<Directory> Directories { get; set; } = new List<Directory>();  
}