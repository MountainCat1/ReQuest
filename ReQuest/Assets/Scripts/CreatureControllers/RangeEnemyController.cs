using UnityEngine;

namespace CreatureControllers
{
    public class RangeEnemyController : AiController
    {
        private Creature _target;

        [SerializeField] private bool moveOnAttackCooldown = false;

        private void Update()
        {
            Creature.SetMovement(Vector2.zero);
        
            if(Creature.Weapon.OnCooldown && !moveOnAttackCooldown)
                return;
        
            if (!_target)
            {
                _target = GetNewTarget();

                if (!_target)
                {
                    return;
                }
            }

            if (IsInRange(_target, Creature.Weapon.Range) && PathClear(_target, 0.5f)) // TODO: Magic number, its the radius of the creature of a size of a human
            {
                PerformAttack(Creature, _target);
                return;
            }

            PerformMovementTowardsTarget(_target);
        }

        private void PerformAttack(Creature creature, Creature target)
        {
            creature.Weapon.ContiniousAttack(new AttackContext()
            {
                Direction = (target.transform.position - creature.transform.position).normalized,
                Target = target,
                Attacker = Creature
            });
        }
    }
}