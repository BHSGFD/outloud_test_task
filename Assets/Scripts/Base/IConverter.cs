public interface IConverter
{
    public string Serialize(GameSave save);
    public GameSave Deserialize(string data);
}