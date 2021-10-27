using UnityEngine;

public class RowStrategyRight : IRowStrategy
{
    private Vector3 _v = new Vector3(0, 0, 0);
    
    public void Update(Row row)
    {
        _v.z = row.z;
        Block b;
        for (int i = row.head; i != row.tail; i = row.Next(i))
        {
            b = row.blocks[i];
            _v.x = b.GetX() + row.speed;
            b.SetPosition(_v);
        }

        b = row.blocks[row.Prev(row.tail)];

//        if (b.GetX() >= Row.RIGHT_BORDER)
        if (!b.IsVisible())
        {
            row.tail = row.Prev(row.tail);
            row.blocks[row.tail].Deactivate();
        }

        row.Fill();
    }
    
    public void AddBlock(Row row)
    {
        int newHead = row.Prev(row.head);
        Block b = row.InitBlock(newHead);
        if (row.head == row.tail)
        {
            _v.x = Row.RIGHT_BORDER - b.width;
        }
        else
        {
            _v.x = row.blocks[row.head].GetX() - b.offset - b.width;
        }

        _v.z = row.z;
        b.SetPosition(_v);
        row.head = newHead;
    }	

    public bool CanFill(Row row)
    {
        if (row.head == row.tail) {
            return true;
        }
        
        return row.blocks[row.head].GetX() >= Row.LEFT_BORDER;
    }
}
