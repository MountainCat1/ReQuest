public class TouchWeapon : Weapon
{
    protected override void Attack(AttackContext ctx)
    {
        OnHit(ctx.Target, ctx);
    }

    protected override void OnHit(Creature target, AttackContext attackCtx)
    {
        base.OnHit(target, attackCtx);
    }
}