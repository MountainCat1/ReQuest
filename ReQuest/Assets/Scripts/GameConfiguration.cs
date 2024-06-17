using UnityEngine;

public interface IGameConfiguration
{
    public float GameTime { get; }
}

[CreateAssetMenu(fileName = "GameConfiguration", menuName = "GameConfiguration")]
public class GameConfiguration : ScriptableObject, IGameConfiguration
{
    [field: SerializeField] public float GameTime { get; set; }
}