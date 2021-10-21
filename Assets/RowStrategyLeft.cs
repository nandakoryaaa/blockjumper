using UnityEngine;

public class RowStrategyLeft : IRowStrategy
{
    private Vector3 v = new Vector3(0, 0, 0);
    
    public void update(Row row)
    {
        Block b;
        for (int i = row.head; i != row.tail; i = row.next(i)) {
            b = row.blocks[i];
            this.v[0] = b.getX() - row.speed;
            this.v[2] = row.z;
            b.setPosition(this.v);
        }

        b = row.blocks[row.head];

        if (b.getX() + b.width < Row.LEFT_BORDER) {
            row.head = row.next(row.head);
            b.deactivate();
        }

        row.fill();
    }

    public void addBlock(Row row)
    {
        Block b = row.initBlock(row.tail);
        float x;
        if (row.tail == row.head) {
            x = Row.LEFT_BORDER;
        } else {
            Block lastBlock = row.blocks[row.prev(row.tail)];
            x = lastBlock.getX() + lastBlock.width + b.offset;
        }
        b.setPosition(new Vector3(x, 0, row.z));
        row.tail = row.next(row.tail);
    }

    public bool canFill(Row row)
    {
        if (row.head == row.tail) {
            return true;
        }

        Block b = row.blocks[row.prev(row.tail)];

        return (b.getX() + b.width) < Row.RIGHT_BORDER;
    }
}
