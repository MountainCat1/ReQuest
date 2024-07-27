using CreatureControllers;
using UnityEngine;

public class MeleeEnemyController : AiController
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

        if (Vector2.Distance(Creature.transform.position, _target.transform.position) < Creature.Weapon.Range)
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
            Attacker = creature
        });
    }
}