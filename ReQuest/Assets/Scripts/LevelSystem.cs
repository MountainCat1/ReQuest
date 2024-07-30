using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum Characteristics
{
    Strength,
    Dexterity,
    Intelligence,
    Constitution
}

public interface ILevelSystem
{
    event Action ChangedXp;
    event Action ChangedLevel;
    event Action CharacteristicsChanged;
    public int Level { get; }
    public float LevelProgress { get; }
    public int PointsToUse { get; }
    void UpgradeCharacteristic(Characteristics characteristic);
}

public class LevelSystem : ILevelSystem
{
    public const int PointsPerLevel = 1;
    
    public event Action ChangedXp;
    public event Action ChangedLevel;
    public event Action CharacteristicsChanged;
    public int Level { get; private set; }
    public float LevelProgress => GetLevelProgress();

    public int PointsToUse { get; private set; }

    public int Xp { get; private set; }
    
    public Dictionary<Characteristics, int> CharacteristicsLevels = new()
    {
        {Characteristics.Strength, 0},
        {Characteristics.Dexterity, 0},
        {Characteristics.Intelligence, 0},
        {Characteristics.Constitution, 0}
    };

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
                PointsToUse += PointsPerLevel;
                ChangedLevel?.Invoke();
                Debug.Log($"Level up! New level: {Level}");
            }
        }
        
        ChangedXp?.Invoke();
    }
    
    public void UpgradeCharacteristic(Characteristics characteristic)
    {
        if (PointsToUse == 0)
        {
            Debug.LogError("No points to use");
            return;
        }
        
        CharacteristicsLevels[characteristic]++;
        PointsToUse--;

        Debug.Log($"Upgraded {characteristic} to {CharacteristicsLevels[characteristic]}");
        CharacteristicsChanged?.Invoke();
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