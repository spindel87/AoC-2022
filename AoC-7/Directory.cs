

public class Directory
{
    public string Name { get; set; }

    public Directory ParentDirectory { get; set; }

    public IList<Directory> Directories { get; set; } = new List<Directory>();

    public IList<File> Files { get; set; } = new List<File>();

    public int GetSize()
    {
        var sum = 0;
        foreach (var directory in Directories)
            sum += directory.GetSize();

        foreach (var file in Files)
            sum += file.Size;

        return sum;
    }
}
