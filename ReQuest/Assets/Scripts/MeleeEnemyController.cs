using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class MeleeEnemyController : CreatureController
{
    private Creature _target;

    [Inject] private IPathfinding _pathfinding;

    private void Update()
    {
        if (!_target)
        {
            _target = GetNewTarget();

            if (!_target)
            {
                return;
            }
        }

        if (Vector2.Distance(Creature.transform.position, _target.transform.position) < Creature.DefaultWeapon.Range)
        {
            PerformAttack(Creature, _target);
            return;
        }

        PerformMovementTowardsTarget(_target);
    }

    private void PerformAttack(Creature creature, Creature target)
    {
        creature.DefaultWeapon.Attack(new AttackContext()
        {
            Direction = (target.transform.position - creature.transform.position).normalized,
            Target = target
        });
    }

    private void PerformMovementTowardsTarget(Creature target)
    {
        float radius = Creature.GetComponent<CircleCollider2D>().radius;
        Vector3 creaturePosition = Creature.transform.position;

        List<Vector3> cornerPoints = GetCornerPoints(creaturePosition, radius);

        bool pathClear = true;
        foreach (Vector3 corner in cornerPoints)
        {
            Debug.DrawLine(corner, target.transform.position, Color.blue);
            if (!_pathfinding.IsClearPath(corner, target.transform.position))
                pathClear = false;
        }

        if (pathClear)
        {
            MoveStraightToTarget(target);
            return;
        }

        MoveViaPathfinding(target);
    }

    private List<Vector3> GetCornerPoints(Vector3 center, float radius)
    {
        List<Vector3> cornerPoints = new List<Vector3>
        {
            center + new Vector3(radius, 0, 0), // Right
            center + new Vector3(0, radius, 0), // Up
            center + new Vector3(-radius, 0, 0), // Left
            center + new Vector3(0, -radius, 0) // Down
        };
        return cornerPoints;
    }

    private void MoveViaPathfinding(Creature target)
    {
        var path = _pathfinding.FindPath(Creature.transform.position, target.transform.position);
        if (path.Count == 0)
        {
            return;
        }

        var nextNode = path[0];
        var direction = (nextNode.worldPosition - Creature.transform.position).normalized;
        Creature.SetMovement(direction);
        Debug.DrawLine(Creature.transform.position, nextNode.worldPosition, Color.red);
    }

    private void MoveStraightToTarget(Creature target)
    {
        var direction = (target.transform.position - Creature.transform.position).normalized;
        Creature.SetMovement(direction);
        Debug.DrawLine(Creature.transform.position, target.transform.position, Color.green);
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