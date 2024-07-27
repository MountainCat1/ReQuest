using UnityEngine;

public class TouchWeapon : Weapon
{
    protected override void Attack(AttackContext ctx)
    {
        var hitCtx = new HitContext()
        {
            Attacker = ctx.Attacker,
            Damage = Damage,
            PushForce = ctx.Target ? CalculatePushForce(ctx.Target) : Vector2.zero,
            Target = ctx.Target
        }; 
        
        OnHit(ctx.Target, hitCtx);
    }

    protected override void OnHit(Creature target, HitContext hitContext)
    {
        base.OnHit(target, hitContext);
    }
}