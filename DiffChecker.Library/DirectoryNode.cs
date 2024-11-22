namespace DiffChecker;

public class DirectoryNode
{
    public string Name { get; set; }
    public string Path { get; set; }
    public List<FileNode> Files { get; set; } = [];
    public List<DirectoryNode> Directories { get; set; } = [];
}