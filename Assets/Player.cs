using UnityEngine;

public class Player
{
    public const int IDLE = 0;
    public const int JUMP = 1;
    
    public Block parent = null;
    public Block body = null;

    private int _status = IDLE;
    private Vector3 _position;
    private float _dy = 0;
    private float _gravity = 0;
    private int _dist;
    private int _progress;
    private int _frameCount;
    private float _startZ;
        
    public Player()
    {
        this.body = new Block(0, 0, Row.BLOCK_SIZE, Row.BLOCK_SIZE, new Color32(255, 255, 255, 0));
        _position = this.body.GetPosition();
        _position.y = Row.BLOCK_SIZE;
        this.body.SetPosition(_position);
    }

    public void BeginJump(int dist, float speed, float gravity)
    {
        _status = JUMP;
        _gravity = gravity;
        _dy = speed;
        _dist = dist;
        _progress = 0;
        _startZ = _position.z;
        _frameCount = (int) (speed * 2 / gravity);
        this.parent = null;
    }

    public void EndJump()
    {
        _position.y = Row.BLOCK_SIZE;
        _position.z = _startZ + _dist;
        _status = IDLE;
    }

    public void Update()
    {
        if (this.IsJumping()) {
            _position.z = _startZ + (float) _progress * _dist / _frameCount;
            _progress++;
            _position.y += _dy;
            _dy -= _gravity;
            if (_position.y < Row.BLOCK_SIZE)
			{
                this.EndJump();
            }
            this.body.SetPosition(_position);
        }
    }

    public int GetJumpFrameCount()
    {
        return _frameCount;
    }
    
    public bool IsJumping()
    {
        return _status == JUMP;
    }

    public bool IsAttached()
    {
        return !(this.parent is null);
    }
}
