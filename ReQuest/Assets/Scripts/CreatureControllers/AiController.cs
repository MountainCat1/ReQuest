using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using Zenject;

namespace CreatureControllers
{
    public class AiController : CreatureController
    {
        [Inject] protected IPathfinding _pathfinding;

        protected void PerformMovementTowardsTarget(Creature target)
        {
            float radius = Creature.GetComponent<CircleCollider2D>().radius;
           
            var pathClear = PathClear(target, radius);
            if (pathClear)
            {
                MoveStraightToTarget(target);
                return;
            }

            MoveViaPathfinding(target);
        }

        protected bool PathClear(Creature target, float radius)
        {
            Vector3 creaturePosition = Creature.transform.position;
            List<Vector3> cornerPoints = GetCornerPoints(creaturePosition, radius);

            bool pathClear = true;
            foreach (Vector3 corner in cornerPoints)
            {
                if (!_pathfinding.IsClearPath(corner, target.transform.position))
                    pathClear = false;

                Debug.DrawLine(corner, target.transform.position, Color.blue);
            }

            return pathClear;
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


        protected Creature GetNewTarget()
        {
            var targets = Creature.GetAllVisibleCreatures()
                .Where(x => Creature.GetAttitudeTowards(x) == Attitude.Hostile)
                .Where(x => CanSee(x))
                .ToList();

            // Get closest target
            var target = targets
                .OrderBy(x => Vector2.Distance(Creature.transform.position, x.transform.position))
                .FirstOrDefault();

            return target;
        }

        protected bool IsInRange(Creature creature, float rane)
        {
            return Vector2.Distance(Creature.transform.position, creature.transform.position) < rane;
        }
    }
}