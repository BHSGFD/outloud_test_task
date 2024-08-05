public interface IGameSaveService
{
    public void ChangeSaveType(SaveType saveType, ConvertType convertType);
    public void Save(GameSave save);
    public GameSave Load();
    public SaveType SaveType { get; }
    public ConvertType ConvertType { get; }
}