using UnityEngine;

public class LevelSystem
{
    public int Level { get; private set; }
    public int Xp { get; private set; }

    public void AddXp(int xp)
    {
        Xp += xp;

        for (int i = 0; i < LevelThresholds.Length; i++)
        {
            if (Xp < LevelThresholds[i])
                break;

            if (Level < i)
            {
                Level = i;
                Debug.Log($"Level up! New level: {Level}");
            }
        }
    }

    private int[] LevelThresholds = new int[]
    {
        0, 16, 32, 64
    };
}