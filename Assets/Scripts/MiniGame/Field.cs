public class Field
{
    public Field(CellData[] data, int size)
    {
        FieldGrid = data;
        Size = size;
    }

    public CellData[] FieldGrid { get; private set; }
    public int Size { get; private set; }
}