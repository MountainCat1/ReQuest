using UnityEngine;

namespace Weapons
{
    public class ProjectileWeapon : Weapon
    {
        [SerializeField] private Projectile projectilePrefab;

        [SerializeField] private float projectileSpeed;

        protected override void Attack(AttackContext ctx)
        {
            var direction = ctx.Direction;

            var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Vector2 normalizedDirection = direction.normalized;
            float angle = Mathf.Atan2(normalizedDirection.y, normalizedDirection.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.Euler(0, 0, angle);

            projectile.Initialize(
                speed: projectileSpeed,
                damage: Damage
            );

            projectile.Hit += OnProjectileHit;

            projectile.Launch(ctx);
        }

        private void OnProjectileHit(Creature hitCreature, AttackContext ctx)
        {
            OnHit(hitCreature, ctx);
        }
    }
}