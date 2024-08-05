using System;
using System.Text;
using UnityEngine;

public class Base64Converter : IConverter
{
    public string Serialize(GameSave save)
    {
        var bytes = Encoding.UTF8.GetBytes(JsonUtility.ToJson(save));
        return Convert.ToBase64String(bytes);
    }

    public GameSave Deserialize(string data)
    {
        if (string.IsNullOrEmpty(data)) return new GameSave();

        var bytes = Convert.FromBase64String(data);
        return JsonUtility.FromJson<GameSave>(Encoding.UTF8.GetString(bytes));
    }
}