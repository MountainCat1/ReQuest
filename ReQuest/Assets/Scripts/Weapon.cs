using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [field: SerializeField] public float Range { get; set; }
    [field: SerializeField] public float AttackSpeed { get; set; }
    [field: SerializeField] public float Damage { get; set; }
    [field: SerializeField] public float PushFactor { get; set; }
    
    private DateTime _lastAttackTime;

    public bool OnCooldown => DateTime.Now - _lastAttackTime < TimeSpan.FromSeconds(1f / AttackSpeed);

    public void PerformAttack(AttackContext ctx)
    {
        _lastAttackTime = DateTime.Now;
        Attack(ctx);
    }

    public void ContiniousAttack(AttackContext ctx)
    {
        if (OnCooldown)
            return;

        PerformAttack(ctx);
    }

    protected abstract void Attack(AttackContext ctx);
    
    protected Vector2 CalculatePushForce(Creature target)
    {
        var direction = (target.transform.position - transform.position).normalized;
        var pushForce = direction * (PushFactor * (Damage / target.Health.MaxValue));
        return pushForce;
    }
    
}

public record AttackContext
{
    public Vector2 Direction { get; set; }
    public Creature Target { get; set; }
}