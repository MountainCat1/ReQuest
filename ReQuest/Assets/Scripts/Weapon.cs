using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract void Attack(AttackContext ctx);
}

public record AttackContext
{
    public Vector2 Direction { get; set; }
}