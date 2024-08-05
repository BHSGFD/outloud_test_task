using System;

[Serializable]
public class GameSave
{
    public GameOptions GameOptions;

    public GameSave()
    {
        GameOptions = new GameOptions();
    }
}