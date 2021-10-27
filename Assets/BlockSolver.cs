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

            player.EndFall();
            player.Attach(b);

            break;
		}

        if (!player.IsAttached())
        {
            player.BeginFall();
        }
    }
}
