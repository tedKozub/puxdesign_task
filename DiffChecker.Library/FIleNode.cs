namespace DiffChecker;

public class FileNode
{
    public string Name { get; set; }
    public string Path { get; set; }
    public uint Version { get; set; }
    public DateTime LastModified { get; set; }
}
