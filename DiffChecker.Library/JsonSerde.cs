using Newtonsoft.Json;

namespace DiffChecker;

public static class JsonSerde
{
    private const string JsonSaveFilePath = @"C:\Users\teddy\Documents\analysis.json"; // TODO: extract to config

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