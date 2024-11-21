namespace DiffChecker;

// TODO: add DirectoryNode class later to represent the directory structure as a tree
public class FileNode
{
    public string Name { get; set; }
    public uint Version { get; set; }
    public DateTime LastModified { get; set; }
}
