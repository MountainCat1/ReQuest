using System;
using Managers;
using UnityEngine;
using Zenject;

public abstract class Weapon : MonoBehaviour
{
    [Inject] private ISoundPlayer _soundPlayer;

    [field: SerializeField] public float Range { get; set; }
    [field: SerializeField] public float AttackSpeed { get; set; }
    [field: SerializeField] public float Damage { get; set; }
    [field: SerializeField] public float PushFactor { get; set; }
    [field: SerializeField] public AudioClip HitSound { get; set; }

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

    protected virtual void OnHit(Creature target, AttackContext attackCtx)
    {
        var hitContext = new HitContext()
        {
            Attacker = attackCtx.Attacker,
            Target = attackCtx.Target,
            Damage = Damage,
            PushForce = CalculatePushForce(target)
        };
        
        target.Damage(hitContext);
        
        if (HitSound != null)
            _soundPlayer.PlaySound(HitSound, transform.position);
    }

    protected Vector2 CalculatePushForce(Creature target)
    {
        var direction = (target.transform.position - transform.position).normalized;
        var pushForce = direction * (PushFactor * (Damage / target.Health.MaxValue));
        return pushForce;
    }
}


public struct AttackContext
{
    public Vector2 Direction { get; set; }
    public Creature Target { get; set; }
    public Creature Attacker { get; set; }
}

public struct HitContext
{
    public Creature Attacker { get; set; }
    public Creature Target { get; set; }
    public float Damage { get; set; }
    public Vector2 PushForce { get; set; }
}