using UnityEngine;

public class TouchWeapon : Weapon
{
    protected override void Attack(AttackContext ctx)
    {
        ctx.Target.Damage(Damage, CalculatePushForce(ctx.Target));
        OnHit(ctx.Target);
    }
}