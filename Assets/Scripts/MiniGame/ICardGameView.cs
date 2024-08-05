public interface ICardGameView
{
    public void Prepare(CardMiniGameData data);
    public void InitField(CardMiniGameData data);

    public void ShuffleCard(CardMiniGameData data);
    public void Lock();
    public void Unlock();
}