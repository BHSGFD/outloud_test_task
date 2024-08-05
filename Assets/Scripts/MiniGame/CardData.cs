using System.Collections.Generic;

public class CardMiniGameData
{
    public CardMiniGameData(int size, List<Card> cards)
    {
        Size = size;
        Cards = cards;
    }

    public int Size { get; }

    public List<Card> Cards { get; }
}