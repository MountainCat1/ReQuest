using System;
using UnityEngine;

public interface ILevelSystem
{
    event Action ChangedXp;
    event Action ChangedLevel;
    public int Level { get; }
    public float LevelProgress { get; }
}

public class LevelSystem : ILevelSystem
{
    
    public event Action ChangedXp;
    public event Action ChangedLevel;
    public int Level { get; private set; }
    public float LevelProgress => GetLevelProgress();

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
                ChangedLevel?.Invoke();
                Debug.Log($"Level up! New level: {Level}");
            }
        }
        
        ChangedXp?.Invoke();
    }
    
    private int[] LevelThresholds = new int[]
    {
        0, 16, 32, 64
    };
    
    private float GetLevelProgress()
    {
        if(Level == 0)
            return Xp / (float)LevelThresholds[1];
        
        var a = Xp - LevelThresholds[Level];
        var b = (float)LevelThresholds[Level + 1] - LevelThresholds[Level];
        
        return a / b;
    }
}