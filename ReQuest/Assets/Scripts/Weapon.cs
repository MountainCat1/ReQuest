using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract void Attack(AttackContext ctx);

    [field: SerializeField] public float Range { get; set; }
    [field: SerializeField] public float AttackSpeed { get; set; }
    [field: SerializeField] public float Damage { get; set; }
}

public record AttackContext
{
    public Vector2 Direction { get; set; }
    public Creature Target { get; set; }
}