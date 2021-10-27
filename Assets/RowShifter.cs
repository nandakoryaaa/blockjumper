using UnityEngine;

public class RowShifter
{
    private int _frameCount;
    private int _dist;
    private int _progress;
    private bool _isActive;
    
    /**
     * @param dist дистанция сдвига
     * @param framecount количество кадров, за которое нужно сдвинуть
     */
    public void Start(int dist, int frameCount)
    {
        _dist = dist;
        _frameCount = frameCount;
        _progress = 0;
        _isActive = true;
    }

    public void Stop()
    {
        _isActive = false;
        //_progress = 0;
    }
    
    public void Update(Row[] rows)
    {
        if (!this.IsActive())
        {
            return;
        }

        foreach (Row row in rows)
        {
            row.OffsetZ((float) -_progress * _dist / _frameCount);
        }

        _progress++;
        if (_progress > _frameCount)
        {
            this.Stop();
            this.ShiftRows(rows);
        }
    }

    public void ShiftRows(Row[] rows)
    {
        int len = rows.Length - 1;
        Block[] blocks = rows[0].blocks;
        
        for (int i = 0; i < len; i++)
        {
            rows[i].CopyFrom(rows[i + 1]);
            rows[i].OffsetZ(0);
        }

        Row row = rows[len];
        row.OffsetZ(0);
        row.strategy = rows[len - 2].strategy; // will not work for rows < 3
        row.blocks = blocks;
        row.Reset();
        row.Fill();
    }

    public bool IsActive()
    {
        return _isActive;
    }
}
