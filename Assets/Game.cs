using UnityEngine;

public class Game : MonoBehaviour
{
    public System.Random random;
    private Row[] rows;
    private Player player;
	private RowShifter rowShifter;
	private int dist = Row.SPACING;

    public void Start()
    {
        this.random = new System.Random();
        this.rows = new Row[]
        {
            new Row(this, 0, new RowStrategyStatic()),
            new Row(this, 1, new RowStrategyRight()),
            new Row(this, 2, new RowStrategyLeft()),
            new Row(this, 3, new RowStrategyRight()),
            new Row(this, 4, new RowStrategyLeft())
        };
		
		this.player = new Player();
		this.player.body.setY(Row.BLOCK_SIZE);
		Row row = this.rows[0];
		this.player.parent = row.blocks[row.head];
		this.rowShifter = new RowShifter();
    }

    void Update()
    {
        foreach (Row row in this.rows) {
            row.update();
        }
		if (this.player.isJumping()) {
			this.player.update();
			this.rowShifter.update(this.rows);
		} else if (Input.GetMouseButtonDown(0)) {
			this.player.beginJump(this.dist, 0.3f, 0.01f);
			int count = this.player.getJumpFrameCount();
			if (this.dist == 0) {
				this.rowShifter.start(Row.SPACING, count);
			}
			this.dist = 0;		
		}
    }
    
    void Awake () {
        //QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
}
