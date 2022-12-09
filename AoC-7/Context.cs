// See https://aka.ms/new-console-template for more information
// 1325919

//using var reader = new StreamReader("ex.txt");






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