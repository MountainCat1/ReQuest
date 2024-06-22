using System;
using System.Linq;
using UnityEngine;
using Zenject;

public class MeleeEnemyController : CreatureController
{
    private Creature _target;

    [Inject] private IPathfinding _pathfinding;
    
    private void Update()
    {
        if(!_target)
        {
            _target = GetNewTarget();
            
            if(!_target)
            {
                return;
            }
        }
        
        if(Vector2.Distance(Creature.transform.position, _target.transform.position) < Creature.DefaultWeapon.Range)
        {
            InvokeAttack(Creature, _target);
            return;
        }
        
        MoveTowardsCreature(_target);
    }

    private void InvokeAttack(Creature creature, Creature target)
    {
        creature.DefaultWeapon.Attack(new AttackContext()
        {
            Direction = (target.transform.position - creature.transform.position).normalized,
            Target = target
        });
    }

    private void MoveTowardsCreature(Creature target)
    {
        var direction = (target.transform.position - Creature.transform.position).normalized;
        Creature.SetMovement(direction);
    }

    private Creature GetNewTarget()
    {
        var targets = Creature.GetAllVisibleCreatures()
            .Where(x => Creature.GetAttitudeTowards(x) == Attitude.Hostile)
            .ToList();
        
        // Get closest target
        var target = targets
            .OrderBy(x => Vector2.Distance(Creature.transform.position, x.transform.position))
            .FirstOrDefault();

        return target;
    }
}