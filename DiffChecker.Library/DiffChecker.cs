namespace DiffChecker;

public static class DiffChecker
{
    public static DirectoryDiff? Run(string path)
    {
        if (!JsonSerde.CheckIfJsonExists())
        {
            var nodes = GenerateSnapshot(path);
            JsonSerde.SaveJson(nodes);
            return null;
        }

        if (JsonSerde.LoadJson().Path != path)
        {
            var nodes = GenerateSnapshot(path);
            JsonSerde.SaveJson(nodes);
            return null;
        }

        var oldSnapshot = JsonSerde.LoadJson();
        var newSnapshot = GenerateSnapshot(path);
        var diff = SnapshotComparer.ReturnFlatDirectoryDiff(oldSnapshot, newSnapshot);
        var updatedSnapshot = UpdateFileVersions(oldSnapshot, newSnapshot);
        JsonSerde.SaveJson(updatedSnapshot);
        return diff;
    }

    private static DirectoryNode GenerateSnapshot(string rootPath)
    {
        var directoryNode = new DirectoryNode
        {
            Name = Path.GetFileName(rootPath),
            Path = rootPath
        };

        foreach (var filePath in Directory.GetFiles(rootPath))
        {
            directoryNode.Files.Add(CreateFileNode(filePath));
        }

        foreach (var subdirPath in Directory.GetDirectories(rootPath))
        {
            directoryNode.Directories.Add(GenerateSnapshot(subdirPath));
        }

        return directoryNode;
    }

    private static FileNode CreateFileNode(string filePath)
    {
        return new FileNode
        {
            Name = Path.GetFileName(filePath),
            Path = filePath,
            Version = 1,
            LastModified = File.GetLastWriteTime(filePath)
        };
    }

    private static DirectoryNode UpdateFileVersions(DirectoryNode oldSnapshot, DirectoryNode newSnapshot)
    {
        foreach (var newFile in newSnapshot.Files)
        {
            var oldFile = oldSnapshot.Files.FirstOrDefault(f => f.Name == newFile.Name);

            if (oldFile == null)
            {
                newFile.Version = 1;
            }
            else if (newFile.LastModified != oldFile.LastModified)
            {
                newFile.Version = oldFile.Version + 1;
            }
            else
            {
                newFile.Version = oldFile.Version;
            }
        }

        foreach (var newSubdirectory in newSnapshot.Directories)
        {
            var oldSubdirectory = oldSnapshot.Directories.FirstOrDefault(sd => sd.Name == newSubdirectory.Name);
            UpdateFileVersions(
                oldSubdirectory ?? new DirectoryNode { Name = newSubdirectory.Name, Files = [], Directories = [] },
                newSubdirectory
            );
        }

        return newSnapshot;
    }
}