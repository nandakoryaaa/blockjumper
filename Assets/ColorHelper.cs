using UnityEngine;
/**
 * Вспомогательный класс для работы с цветом
 */

public class ColorHelper
{
    public static Color32 CreateColor(System.Random rand)
    {
        // Цвет изменяется ступенями по 20 единиц, чтобы сделать оттенки более явно различимыми.
        // Также сгенерированные числа никогда не примут значений 128 или 255,
        // что исключает появление серого или белого цвета.
        // В более сложной версии можно передавать список исключаемых цветов и соответственно поменять алгоритм генерации
        return new Color32
        (
            (byte) (rand.Next(12) * 20),
            (byte) (rand.Next(12) * 20),
            (byte) (rand.Next(12) * 20),
            0
        );
    }
}
