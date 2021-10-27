using UnityEngine;

public class BlockSolver
{
    public void Solve(Player player, Row row)
    {
        float pX = player.body.GetX();
        int pWidth = player.body.width;
        int pHalfWidth = pWidth / 2;

        /**
         * Проверить блоки в текущем ряду на геометрическое пересечение с игроком.
         * Проверка не делается через физику и коллизии, чтобы условие всегда срабатывало точно.
         */
        for (int i = row.head; i != row.tail; i = row.Next(i))
        {
            Block b = row.blocks[i];
            float bX = b.GetX();
            int bWidth = b.width;

            if ((bX + bWidth < pX + pHalfWidth) || (bX > pX + pHalfWidth))
            {
                continue;
            }

            player.Attach(b, pX - bX);

            break;
        }

        // Если после проверки всех блоков игрок остался неприкреплённым, он начинает падать
        if (!player.IsAttached())
        {
            player.BeginFall();
        }
    }
}
