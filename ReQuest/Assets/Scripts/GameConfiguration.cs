using UnityEngine;

public interface IGameConfiguration
{
    public float GameTime { get; }
    
    // UI
    public float TypingSpeed { get; }
    public float TypingDelay => 1f / TypingSpeed;
}

[CreateAssetMenu(fileName = "GameConfiguration", menuName = "GameConfiguration")]
public class GameConfiguration : ScriptableObject, IGameConfiguration
{
    [field: SerializeField] public float GameTime { get; private set; }
    [field: SerializeField] public float TypingSpeed { get; private set; }
}