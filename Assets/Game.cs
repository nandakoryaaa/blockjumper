using UnityEngine;

public class Game : MonoBehaviour
{
    public System.Random random;

    private Row[] _rows;
    private Player _player;
    private RowShifter _rowShifter;
    private int _dist = Row.SPACING;
    private BlockSolver _solver;

    public void Start()
    {
        this.random = new System.Random();
        _rows = new Row[]
        {
            new Row(this, 0, new RowStrategyStatic()),
            new Row(this, 1, new RowStrategyLeft()),
            new Row(this, 2, new RowStrategyRight()),
            new Row(this, 3, new RowStrategyLeft()),
            new Row(this, 4, new RowStrategyRight()),
        };
        
        _player = new Player();
        _player.body.SetY(Row.BLOCK_SIZE);
        Row row = _rows[0];
        _player.parent = row.blocks[row.head];
        _rowShifter = new RowShifter();
        _solver = new BlockSolver();
    }

    void Update()
    {
        foreach (Row row in _rows)
        {
            row.Update();
        }

        if (_player.IsJumping())
        {
            // Если игрок находится в прыжыке, надо двигать ряды ему навстречу
            _rowShifter.Update(_rows);
        }

        _player.Update();

        if (_player.IsUnsolved())
        {
            // Солвер решает, что делать дальше с игроком
            _solver.Solve(_player, _rows[1]);
        }
        
        if (_player.IsDead())
        {
            Block b = _rows[1].GetLastBlock();
            _player.Attach(b, (b.width - _player.body.width) / 2);
        }
    
        // Прыжок можно начать, если игрок стоит на блоке
        if (_player.IsAttached() && Input.GetMouseButtonDown(0))
        {
            // Дистанция прыжка при первом прыжке ненулевая, т.к. игрок прыгает с первого ряда на второй
            _player.BeginJump(_dist, 0.3f, 0.01f);
            if (_dist == 0)
            {
                // С нулевой дистанцией игрок прыгает на месте, а ряды движутся ему навстречу
                _rowShifter.Start(Row.SPACING, _player.GetJumpFrameCount());
            }
            _dist = 0;
        }
    }
    
    void Awake ()
    {
        //QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
}
