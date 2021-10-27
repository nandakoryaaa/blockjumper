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

        bool playerJumpState = _player.IsJumping();
	
		if (playerJumpState)
		{
            _rowShifter.Update(_rows);
		}

    	_player.Update();

        if (!_player.IsJumping() && playerJumpState)
        {
            _solver.Solve(_player, _rows[1]);
        }
	
        if (!_player.IsJumping() && Input.GetMouseButtonDown(0))
		{
			_player.BeginJump(_dist, 0.3f, 0.01f);
			if (_dist == 0)
			{
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
