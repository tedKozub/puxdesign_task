using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace DiffChecker;

public class DiffChecker
{
    private const string JsonSaveFilePath = @"C:\Users\teddy\Documents\analysis.json"; // TODO: extract to config
    
    
    public void Run(string path)
    {
        // validate the path, check if not empty, wrong or if it is even a valid path and not random data
        
        
        
        if (!CheckIfJsonExists())
        {
            var nodes = AnalyzeDirectory(path);
            SaveJSON(nodes); // TODO: make the serde stuff a new static(?) class
            return;
        }
        // if JSON exists, load the JSON, run analysis and compare the two trees
        var oldDirState = LoadJson();
        var newDirState = AnalyzeDirectory(path);
        CompareDirectories(oldDirState, newDirState);
        // diffChecker.SaveJSON();
        // diffChecker.Compare();
        // TODO: return a JSON object with the diff results
    }

    private void CompareDirectories(List<FileNode> oldDirState, List<FileNode> newDirState)
    {
        // compare the two trees
        foreach (var oldNode in oldDirState)
        {
            var newNode = newDirState.Find(n => n.Name == oldNode.Name);
            if (newNode == null)
            {
                // file was deleted
                Console.WriteLine($"File {oldNode.Name} was deleted");
            }
            else if (oldNode.LastModified != newNode.LastModified)
            {
                // file was modified
                Console.WriteLine($"File {oldNode.Name} was modified");
            }
        }

        foreach (var newNode in newDirState)
        {
            var oldNode = oldDirState.Find(n => n.Name == newNode.Name);
            if (oldNode == null)
            {
                // file was added
                Console.WriteLine($"File {newNode.Name} was added");
            }
        }
    }

    private List<FileNode> AnalyzeDirectory(string path)
    {
        var nodes = new List<FileNode>();
        var files = Directory.GetFiles(path);
        foreach (var file in files)
        {
            nodes.Add(new FileNode
            {
                Name = file,
                Version = 1,
                LastModified = File.GetLastWriteTime(file)
            });
        }

        return nodes;
    }

    public bool CheckIfJsonExists()
    {
        // check if the JSON file exists
        if (File.Exists(JsonSaveFilePath))
        {
            return true;
        }
        return false;
    }

    private List<FileNode> LoadJson()
    {
        var json = File.ReadAllText(JsonSaveFilePath);
        var nodes = JsonConvert.DeserializeObject<List<FileNode>>(json);
        return nodes;
    }
    
    public void SaveJSON(List<FileNode> nodes)
    {
        // serialize the nodes to JSON and save to local file
        var json = JsonConvert.SerializeObject(nodes);
        File.WriteAllText(JsonSaveFilePath, json);
    }
    
}