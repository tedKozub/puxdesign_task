namespace DiffChecker;

public class DirectoryDiff
{
    public List<FileNode> AddedFiles { get; set; } = [];
    public List<FileNode> ModifiedFiles { get; set; } = [];
    public List<FileNode> DeletedFiles { get; set; } = [];

    public List<DirectoryNode> AddedDirectories { get; set; } = [];
    public List<DirectoryNode> DeletedDirectories { get; set; } = [];
    public List<DirectoryDiff> SubdirectoryDiffs { get; set; } = [];
}