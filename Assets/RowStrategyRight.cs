using UnityEngine;

public class RowStrategyRight : IRowStrategy
{
    private Vector3 v = new Vector3(0, 0, 0);
    public void update(Row row)
    {
        Block b;
        for (int i = row.head; i != row.tail; i = row.next(i)) {
            b = row.blocks[i];
            this.v[0] = b.getX() + row.speed;
            this.v[2] = row.z;
            b.setPosition(this.v);
        }

        b = row.blocks[row.prev(row.tail)];
        if (b.getX() >= Row.RIGHT_BORDER) {
            row.tail = row.prev(row.tail);
            row.blocks[row.tail].deactivate();
        }

        row.fill();
    }
    
    public void addBlock(Row row)
    {
        int newHead = row.prev(row.head);
        Block b = row.initBlock(newHead);
        if (row.head == row.tail) {
            this.v[0] = Row.RIGHT_BORDER - b.width;
        } else {
            this.v[0] = row.blocks[row.head].getX() - b.offset - b.width;
        }

        this.v[2] = row.z;
        b.setPosition(this.v);
        row.head = newHead;
    }	

    public bool canFill(Row row)
    {
        if (row.head == row.tail) {
            return true;
        }
        
        return row.blocks[row.head].getX() >= Row.LEFT_BORDER;
    }
}
