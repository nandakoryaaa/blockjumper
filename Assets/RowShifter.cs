using UnityEngine;

public class RowShifter
{
    private int _frameCount;
    private int _dist;
    private int _progress;
    private bool _isActive;
    
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
    }
    
    public void Update(Row[] rows)
    {
        if (!_isActive)
        {
            return;
        }

        foreach (Row row in rows)
        {
            row.SetZ((float) -_progress * _dist / _frameCount);
        }

        _progress++;
        if (_progress > _frameCount)
        {
            _progress = 0;
            _isActive = false;
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
            rows[i].SetZ(0);
        }

        Row row = rows[len];
        row.SetZ(0);
        row.strategy = rows[len - 2].strategy; // will not work for rows < 3
        row.blocks = blocks;
        row.Reset();
        row.Fill();
    }
}
