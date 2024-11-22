using Newtonsoft.Json;

namespace DiffChecker;

public static class JsonSerde
{
    // save it inside the project directory, not the bin directory
    private static string JsonSaveFilePath => Path.Combine(Directory.GetCurrentDirectory(), "snapshot.json");
    
    
    public static bool CheckIfJsonExists()
    {
        return File.Exists(JsonSaveFilePath);
    }

    public static DirectoryNode LoadJson()
    {
        var json = File.ReadAllText(JsonSaveFilePath);
        return JsonConvert.DeserializeObject<DirectoryNode>(json);
    }

    public static void SaveJson(DirectoryNode nodes)
    {
        var json = JsonConvert.SerializeObject(nodes);
        File.WriteAllText(JsonSaveFilePath, json);
    }
}