using System.Collections.Generic;

public interface IFieldProvider
{
    public void InitField(CardMiniGameData data);
    public void UpdateField(CardMiniGameData data);
    public void ClearCell(List<CellData> cell);
    public CellData GetCell(int x, int y);
    public Field Field { get; }
}