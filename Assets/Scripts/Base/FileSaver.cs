using System.IO;
using UnityEngine;

public class FileSaver : ISaver
{
    public void Save(string data)
    {
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "gamesave.dat"), data);
    }

    public string Load()
    {
        string data = null;
        var filePath = Path.Combine(Application.persistentDataPath, "gamesave.dat");
        if (File.Exists(filePath)) data = File.ReadAllText(filePath);
        return data;
    }
}