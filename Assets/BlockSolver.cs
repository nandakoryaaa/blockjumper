using UnityEngine;

public class BlockSolver
{
    public void Solve(Player player, Row row)
    {
        float pX = player.body.GetX();
        int pWidth = player.body.width;
        int pHalfWidth = pWidth / 2;

        for (int i = row.head; i != row.tail; i = row.Next(i))
        {
            Block b = row.blocks[i];
            float bX = b.GetX();
            int bWidth = b.width;

            if ((bX + bWidth < pX + pHalfWidth) || (bX > pX + pHalfWidth))
            {
                continue;
            }

            player.Attach(b);
            player.DisablePhysics();
            break;
		}

        if (!player.IsAttached())
        {
            player.body.SetY(Row.BLOCK_SIZE + 0.01f);
            player.EnablePhysics();
            for (int i = row.head; i != row.tail; i = row.Next(i))
            {
                row.blocks[i].EnableCollisions();
            }
        }
    }
}
