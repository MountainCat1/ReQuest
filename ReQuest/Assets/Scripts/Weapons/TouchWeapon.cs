public class TouchWeapon : Weapon
{
    public override void Attack(AttackContext ctx)
    {
        ctx.Target.Damage(Damage);
    }
}