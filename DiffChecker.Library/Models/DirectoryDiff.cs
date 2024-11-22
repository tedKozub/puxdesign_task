namespace DiffChecker;

public class DirectoryDiff
{
    public List<FileNode> AddedFiles { get; set; } = [];
    public List<FileNode> ModifiedFiles { get; set; } = [];
    public List<FileNode> DeletedFiles { get; set; } = [];

    public List<string> AddedDirectories { get; set; } = [];
    public List<string> DeletedDirectories { get; set; } = [];
}
