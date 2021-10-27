using UnityEngine;

public class Row
{
    public const int MAX_BLOCKS = 10;
    public const int BLOCK_SIZE = 2;
    public const int BLOCK_SIZE_HALF = Row.BLOCK_SIZE / 2;
    public const int BLOCK_SIZE_HALF3 = BLOCK_SIZE_HALF * 3;
    public const int BLOCK_SIZE2 = BLOCK_SIZE * 2;
    public const int SPACING = BLOCK_SIZE * 2;
    public const int LEFT_BORDER = -16;
    public const int RIGHT_BORDER = 22;

    public int num = 0;
    public int head = 0;
    public int tail = 0;
    public float speed = 0.1f;
    public float z = 0;

    public IRowStrategy strategy = null;
    public Block[] blocks = new Block[MAX_BLOCKS];
    
    private System.Random _rand = null;

    public Row(Game game, int num, IRowStrategy strategy)
    {
        this._rand = game.random;
        this.num = num;
        this.strategy = strategy;
        this.OffsetZ(0);
        this.Fill();
    }

    public void OffsetZ(float offset)
    {
        this.z = this.num * SPACING + offset;
    }
    
    public void Reset()
    {
        this.head = 0;
        this.tail = 0;
        foreach (Block b in this.blocks)
        {
            if (!(b is null))
            {
                b.Deactivate();
            }
        }
    }

    public Block InitBlock(int pos)
    {
        int width = BLOCK_SIZE_HALF3 + _rand.Next(4) * Row.BLOCK_SIZE_HALF;
        int offset = Row.BLOCK_SIZE_HALF3 + _rand.Next(4) * Row.BLOCK_SIZE_HALF;
        Color32 color = ColorHelper.CreateColor(_rand);
        
        if (this.blocks[pos] is null)
        {
            this.blocks[pos] = new Block(offset, this.z, BLOCK_SIZE, width, color);
        }
        else
        {
            this.blocks[pos].Reshape(offset, width, color);
        }

        this.blocks[pos].Activate();

        return this.blocks[pos];
    }
    
    public void Update()
    {
        this.strategy.Update(this);
    }

    public void Fill()
    {
        while (this.strategy.CanFill(this))
        {
            this.strategy.AddBlock(this);
        }
    }

    public int Next(int pos)
    {
        pos++;
        return pos < this.blocks.Length ? pos : 0;
    }

    public int Prev(int pos)
    {
        pos--;
        return pos < 0 ? this.blocks.Length - 1 : pos;
    }

    public void CopyFrom(Row row)
    {
        this.blocks = row.blocks;
        this.strategy = row.strategy;
        this.head = row.head;
        this.tail = row.tail;
    }

    public Block GetLastBlock()
    {
        return this.strategy.GetLastBlock(this);
    }
}
