using UnityEngine;

public class JsonConverter : IConverter
{
    public string Serialize(GameSave save)
    {
        return JsonUtility.ToJson(save);
    }

    public GameSave Deserialize(string data)
    {
        if (string.IsNullOrEmpty(data)) return new GameSave();

        return JsonUtility.FromJson<GameSave>(data);
    }
}