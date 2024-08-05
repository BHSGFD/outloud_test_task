using UnityEngine;

public class GameSaveService : IGameSaveService
{
    private ISaver _saver;
    private IConverter _converter;

    public GameSaveService()
    {
        _saver = SaveType == SaveType.File ? new FileSaver() : new PlayerPrefsSaver();
        _converter = ConvertType == ConvertType.Base64 ? new Base64Converter() : new JsonConverter();
    }
   
    public void ChangeSaveType(SaveType saveType, ConvertType convertType)
    {
        GameSave gameSave = Load();
      
        SaveType = saveType;
        ConvertType = convertType;
      
        _saver = SaveType == SaveType.File ? new FileSaver() : new PlayerPrefsSaver();
        _converter = ConvertType == ConvertType.Base64 ? new Base64Converter() : new JsonConverter();
      
        Save(gameSave);
    }

    public void Save(GameSave save)
    {
        _saver.Save(_converter.Serialize(save));
      
    }

    public GameSave Load()
    {
        return _converter.Deserialize(_saver.Load());
    }
   
    public SaveType SaveType
    {
        get { return (SaveType)PlayerPrefs.GetInt(nameof(SaveType), 0); }
        set { PlayerPrefs.SetInt(nameof(SaveType), (int) value); }
    }

    public ConvertType ConvertType
    {
        get { return (ConvertType)PlayerPrefs.GetInt(nameof(ConvertType), 0); }
        set { PlayerPrefs.SetInt(nameof(ConvertType), (int) value); }
    }
}