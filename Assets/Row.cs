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
    private System.Random rand = null;

    public Row(Game game, int num, IRowStrategy strategy)
    {
        this.rand = game.random;
        this.num = num;
		this.setDepthOffset(0);
        this.strategy = strategy;
        this.fill();
    }

	public void setDepthOffset(float offset)
	{
		this.z = this.num * SPACING + offset;
	}
	
    public void reset()
    {
         this.head = 0;
         this.tail = 0;
         for (int i = 0; i < MAX_BLOCKS; i++) {
            if (!(this.blocks[i] is null)) {
				this.blocks[i].deactivate();
		 	}
         }
    }
    
    public Block initBlock(int pos)
    {
        int width = BLOCK_SIZE_HALF3 + rand.Next(4) * Row.BLOCK_SIZE_HALF;
        int offset = Row.BLOCK_SIZE_HALF3 + rand.Next(4) * Row.BLOCK_SIZE_HALF;
        Color32 color = new Color32((byte) rand.Next(256), (byte) rand.Next(256), (byte) rand.Next(256), 0);

        if (this.blocks[pos] is null) {
            this.blocks[pos] = new Block(offset, this.z, BLOCK_SIZE, width, color);
        } else {
			this.blocks[pos].update(offset, width, color);
        }

        this.blocks[pos].activate();

        return this.blocks[pos];
    }
    
    public void update()
    {
        this.strategy.update(this);
    }

    public void fill()
    {
        while (this.strategy.canFill(this)) {
            this.strategy.addBlock(this);
        }
    }

    public int next(int pos)
    {
        pos++;
        return pos < this.blocks.Length ? pos : 0;
    }

    public int prev(int pos)
    {
        pos--;
        return pos < 0 ? this.blocks.Length - 1 : pos;
    }
}
