using UnityEngine;

public class RowStrategyLeft : IRowStrategy
{
    private Vector3 _v = new Vector3(0, 0, 0);
    
    public void Update(Row row)
    {
        _v.z = row.z;
        Block b;
        for (int i = row.head; i != row.tail; i = row.Next(i)) {
            b = row.blocks[i];
            _v.x = b.GetX() - row.speed;
 
            b.SetPosition(_v);
        }

        b = row.blocks[row.head];

//      if (b.GetX() + b.width < Row.LEFT_BORDER)
        if (!b.IsVisible())
        {
            row.head = row.Next(row.head);
            b.Deactivate();

        }

        row.Fill();
    }

    public void AddBlock(Row row)
    {
        Block b = row.InitBlock(row.tail);

        if (row.tail == row.head)
        {
            _v.x = Row.LEFT_BORDER;
        }
        else
        {
            Block lastBlock = row.blocks[row.Prev(row.tail)];
            _v.x = lastBlock.GetX() + lastBlock.width + b.offset;
        }

        _v.z = row.z;
        b.SetPosition(_v);
        row.tail = row.Next(row.tail);
    }
    
    public bool CanFill(Row row)
    {
        if (row.head == row.tail) {
            return true;
        }

        Block b = row.blocks[row.Prev(row.tail)];

        return (b.GetX() + b.width) < Row.RIGHT_BORDER;
    }

    public Block GetLastBlock(Row row)
    {
        return row.blocks[row.Prev(row.tail)];
    }

}
