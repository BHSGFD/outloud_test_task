using UnityEngine;

public class PlayerPrefsSaver : ISaver
{
    public void Save(string data)
    {
        PlayerPrefs.SetString("GameSave", data);
        PlayerPrefs.Save();
    }

    public string Load()
    {
        return PlayerPrefs.GetString("GameSave");
    }
}