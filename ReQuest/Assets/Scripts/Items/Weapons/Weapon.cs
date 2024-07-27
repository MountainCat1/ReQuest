using System;
using Items;
using Managers;
using UnityEngine;
using Zenject;

public abstract class Weapon : ItemBehaviour
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

    protected virtual void OnHit(Creature target, HitContext hitContext)
    {
        if (target != null)
        {
            target.Damage(hitContext);
        }

        if (HitSound != null)
            _soundPlayer.PlaySound(HitSound, transform.position);
    }

    protected Vector2 CalculatePushForce(Creature target)
    {
        var direction = (target.transform.position - transform.position).normalized;
        var pushForce = direction * (PushFactor * (Damage / target.Health.MaxValue));
        return pushForce;
    }

    public override void Use(ItemUseContext ctx)
    {
        base.Use(ctx);

        ctx.Creature.StartUsingWeapon(this);
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

    public void ValidateAndLog()
    {
        if (Attacker == null)
            Debug.LogError("Attacker is null");

        if (Target == null)
            Debug.LogError("Target is null");

        if (Damage <= 0)
            Debug.LogError("Damage is less than or equal to 0");

        if (PushForce == Vector2.zero)
            Debug.LogError("PushForce is zero");
    }
}