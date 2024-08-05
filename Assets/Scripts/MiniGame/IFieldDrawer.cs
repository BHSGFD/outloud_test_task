using System;
using System.Collections.Generic;

public interface IFieldDrawer
{
    public void DrawField(Action onComplete);
    public void Reshuffle(Action onComplete);
    public CardView GetCard(Card card);
    public void OpenAllCards();
    public void CloseAllCards();
    public void Lock();
    public void Unlock();
    public List<CardView> GetCardsInGame();
}