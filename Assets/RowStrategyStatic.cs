using UnityEngine;

public class RowStrategyStatic : IRowStrategy
{
    private Vector3 _v = new Vector3(0, 0, 0);
    
    public void Update(Row row)
    {
        _v.z = row.z;
        Block b;
        for (int i = row.head; i != row.tail; i = row.Next(i))
        {
            b = row.blocks[i];
            _v.x = b.GetX();
            b.SetPosition(_v);
        }
    }

    public void AddBlock(Row row)
    {
        Block b = row.InitBlock(row.head);
        b.SetX(0);
        row.tail = row.Next(row.tail);
    }

    public bool CanFill(Row row)
    {
        return row.head == row.tail;
    }
}