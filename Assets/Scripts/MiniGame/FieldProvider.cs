using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class FieldProvider : IFieldProvider
{
    private DiContainer _container;
    
    public FieldProvider(DiContainer container)
    {
        _container = container;
    }
    
    public void InitField(CardMiniGameData data)
    {
        var size = data.Size;
        
        CellData[] cellData = new CellData[(int)Mathf.Pow(size, 2)];

        for (int row = 0; row < size; row++)
        {
            for (int column = 0; column <  size; column++)
            { 
                cellData[row * size + column] = new CellData(data.Cards[row * size + column] , new Vector2(row, column));   
            }
        }
        
        Field = _container.Instantiate<Field>(new object[]{cellData, size});
    }

    public void UpdateField(CardMiniGameData data)
    {
        int index = 0;

        for (var i = 0; i < Field.FieldGrid.Length; i++)
        {
            var cell = Field.FieldGrid[i];
            
            if (cell.Card == null)
            {
                continue;
            }

            cell.Card = data.Cards[index];
            index++;
        }
    }

    public void ClearCell(List<CellData> cells)
    {
        foreach (var cell in cells)
        {
            Field.FieldGrid.FirstOrDefault(fieldCell => fieldCell == cell)!.Card = null;
        }
    }

    public CellData GetCell(int x, int y)
    {
        return Field.FieldGrid[x * Field.Size + y];
    }

    public Field Field { get; private set; }
}