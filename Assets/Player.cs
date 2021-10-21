using UnityEngine;

public class Player
{
    public const int IDLE = 0;
    public const int JUMP = 1;
    
    public int status = IDLE;
    public Block parent = null;
    public Block body = null;
    public Vector3 position;
    
    private float dy = 0;
    private float gravity = 0;
    private int dist;
    private int progress;
    private int frameCount;
    private float startZ;
        
    public Player()
    {
        this.body = new Block(0, 0, Row.BLOCK_SIZE, Row.BLOCK_SIZE, new Color32(255, 255, 255, 0));
        this.position = this.body.getPosition();
        this.position.y = Row.BLOCK_SIZE;
        this.body.setPosition(this.position);
    }

    public void beginJump(int dist, float speed, float gravity)
    {
        this.status = JUMP;
        this.gravity = gravity;
        this.dy = speed;
        this.dist = dist;
        this.progress = 0;
        this.startZ = this.position.z;
        this.frameCount = (int) (speed * 2 / gravity);
        this.parent = null;
    }

    public void endJump()
    {
        this.position.y = Row.BLOCK_SIZE;
        this.position.z = this.startZ + this.dist;
        this.status = IDLE;
    }

    public void update()
    {
        if (this.isJumping()) {
            this.position.z = this.startZ + (float) this.progress * this.dist / this.frameCount;
            this.progress++;
            this.position.y += this.dy;
            this.dy -= this.gravity;
            if (this.position.y < Row.BLOCK_SIZE) {
                this.endJump();
            } else {
                this.body.setPosition(this.position);
            }
        }
    }

    public int getJumpFrameCount()
    {
        return this.frameCount;
    }
    
    public bool isJumping()
    {
        return this.status == JUMP;
    }

    public bool isAttached()
    {
        return !(this.parent is null);
    }
}
