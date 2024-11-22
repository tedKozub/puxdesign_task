namespace DiffChecker;

public static class SnapshotComparer
{
    public static DirectoryDiff ReturnFlatDirectoryDiff(DirectoryNode oldSnapshot, DirectoryNode newSnapshot)
        {
            var diff = new DirectoryDiff();
            var stack = new Stack<(DirectoryNode oldDir, DirectoryNode newDir)>();

            stack.Push((oldSnapshot, newSnapshot));

            while (stack.Count > 0)
            {
                var (oldDir, newDir) = stack.Pop();
                foreach (var newFile in newDir.Files)
                {
                    var oldFile = oldDir.Files.FirstOrDefault(f => f.Name == newFile.Name);
                    if (oldFile == null)
                    {
                        diff.AddedFiles.Add(newFile);
                    }
                    else if (newFile.LastModified != oldFile.LastModified)
                    {
                        diff.ModifiedFiles.Add(newFile);
                    }
                }

                foreach (var oldFile in oldDir.Files.Where(oldFile => newDir.Files.All(f => f.Name != oldFile.Name)))
                {
                    diff.DeletedFiles.Add(oldFile);
                }
                foreach (var newSubdir in newDir.Directories)
                {
                    var oldSubdir = oldDir.Directories.FirstOrDefault(d => d.Path == newSubdir.Path);
                    if (oldSubdir == null)
                    {
                        diff.AddedDirectories.Add(newSubdir.Path);
                    }
                    else
                    {
                        stack.Push((oldSubdir, newSubdir));
                    }
                }

                foreach (var oldSubdir in oldDir.Directories)
                {
                    if (newDir.Directories.All(d => d.Path != oldSubdir.Path))
                    {
                        diff.DeletedDirectories.Add(oldSubdir.Path);
                    }
                }
            }

            return diff;
        }
}
