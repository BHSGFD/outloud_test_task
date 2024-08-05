using UnityEngine;

public class CellData
{
    public Card Card;

    public Vector2 Position;

    public CellData(Card card, Vector2 position)
    {
        Card = card;
        Position = position;
    }
}