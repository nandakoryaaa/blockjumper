using UnityEngine;

public class RowShifter
{
    public bool isActive;
    private int frameCount;
    private int dist;
    private int progress;

    public void start(int dist, int frameCount)
    {
        this.dist = dist;
        this.frameCount = frameCount;
        this.isActive = true;
        this.progress = 0;
    }

    public void stop()
    {
        this.isActive = false;
    }
    
    public void update(Row[] rows)
    {
        if (!this.isActive) {
            return;
        }

        foreach (Row row in rows) {
            row.setDepthOffset((float) -progress * dist / frameCount);
        }

        this.progress++;
        if (this.progress > this.frameCount) {
            progress = 0;
            this.isActive = false;
            this.shiftRows(rows);
        }
    }

    private void shiftRows(Row[] rows)
    {
        int len = rows.Length - 1;
        Block[] blocks = rows[0].blocks;
        for (int i = 0; i < len; i++) {
            rows[i].blocks = rows[i + 1].blocks;
            rows[i].strategy = rows[i + 1].strategy;
            rows[i].head = rows[i + 1].head;
            rows[i].tail = rows[i + 1].tail;
            rows[i].setDepthOffset(0);
        }

        Row row = rows[len];
        row.setDepthOffset(0);
        row.strategy = rows[len - 2].strategy; // will not work for rows < 3
        row.blocks = blocks;
        row.reset();
        row.fill();
    }
}
