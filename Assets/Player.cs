using UnityEngine;

public class Player
{
    public const int UNDEFINED = 0;
    public const int JUMP = 1;
    public const int UNSOLVED = 2;
    public const int FALL = 3;
    public const int DEAD = 4;
    
    public Block parent = null;
    public Block body = null;

    private int _status = UNDEFINED;
    private Vector3 _position;
    private float _dy = 0;
    private float _gravity = 0;
    private int _dist;
    private int _progress;
    private int _frameCount;
    private float _startZ;

    private Rigidbody _rigidBody;

    public Player()
    {
        this.body = new Block(0, 0, Row.BLOCK_SIZE, Row.BLOCK_SIZE, new Color32(255, 255, 255, 0));
        _position = this.body.GetPosition();
        _position.y = Row.BLOCK_SIZE;
        this.body.SetPosition(_position);

        _rigidBody = this.body.gameObject.AddComponent<Rigidbody>();
        _rigidBody.isKinematic = false;
        _rigidBody.drag = 0;
        _rigidBody.mass = 5000;
        _rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

        this.EndFall();
    }

    /**
     * Если прыжок не удался, включается физика для обработки падения.
     * Rigidbody получает начальную скорость движения как после прыжка.
     */
    public void BeginFall()
    {
        _rigidBody.detectCollisions = true;
        _rigidBody.useGravity = true;
        _rigidBody.velocity = new Vector3(0f, _dy * 50, 0f);
        _status = FALL;
    }

    public void EndFall()
    {
        _rigidBody.detectCollisions = false;
        _rigidBody.useGravity = false;
        _rigidBody.velocity = Vector3.zero;
        _rigidBody.angularVelocity = Vector3.zero;
        this.body.gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
        _status = UNDEFINED;
    }
    /**
     * Прыжок делается без использования физики, чтобы высота, направление и дистанция всегда точно совпадали
     */
    public void BeginJump(int dist, float speed, float gravity)
    {
        this.Detach();
        _gravity = gravity;
        _dy = speed;
        _dist = dist;
        _progress = 0;
        _startZ = _position.z;
        _frameCount = (int) (speed * 2 / gravity);
        _status = JUMP;
    }

    /**
     * В конце прыжка координаты приводятся к целевым
     */
    public void EndJump()
    {
        _position.y = Row.BLOCK_SIZE;
        _position.z = _startZ + _dist;
        _status = UNSOLVED;
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
        else
        {
            if (this.IsAttached())
            {
                // Если родительский блок неактивен, игрок уехал на нём за край экрана
                if (!this.parent.IsActive()) {
                    this.Detach();
                    _status = DEAD;
                }
                else
                {
                    this.body.SetPosition(this.parent.GetPosition() + _position);
                }
            }
            else if (this.IsFalling() && this.body.GetY() < -10)
            {
                // Если игрок упал ниже плинтуса, то он умер
                _status = DEAD;
            }
        }
    }

    /**
     * Сколько кадров займёт прыжок.
     * Требуется для вычисления сдвига рядов в RowShifter
     */
    public int GetJumpFrameCount()
    {
        return _frameCount;
    }

    public void Attach(Block block, float blockOffset)
    {
        this.EndFall();
        this.parent = block;
        // позиция считается локальной относительно блока с момента прикрепления
        _position.x = blockOffset;
        _position.y = Row.BLOCK_SIZE;
        _position.z = 0;
        _status = UNDEFINED;
    }

    public void Detach()
    {
        if (!(this.parent is null)) {
            // После отделения от блока позиция будет считаться глобальной
            _position += parent.GetPosition();
            this.parent = null;
        }
        _status = UNDEFINED;
    }

    public bool IsAttached()
    {
        return !(this.parent is null);
    }

    public bool IsJumping()
    {
        return _status == JUMP;
    }

    public bool IsFalling()
    {
        return _status == FALL;
    }

    public bool IsDead()
    {
        return _status == DEAD;
    }

    public bool IsUnsolved()
    {
        return _status == UNSOLVED;
    }
}
