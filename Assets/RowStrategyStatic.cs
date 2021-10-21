using UnityEngine;

public class RowStrategyStatic : IRowStrategy
{
    private Vector3 v = new Vector3(0, 0, 0);
    public void update(Row row)
    {
        Block b;
        for (int i = row.head; i != row.tail; i = row.next(i)) {
            b = row.blocks[i];
            this.v[0] = b.getX();
            this.v[2] = row.z;
            b.setPosition(this.v);
        }
    }

    public void addBlock(Row row)
    {
        Block b = row.initBlock(row.head);
        b.setX(0);
        row.tail = row.next(row.tail);
    }

    public bool canFill(Row row)
    {
        return row.head == row.tail;
    }
}