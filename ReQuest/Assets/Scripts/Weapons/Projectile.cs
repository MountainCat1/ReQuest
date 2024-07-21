using System;
using UnityEngine;

namespace Weapons
{
    public class Projectile : MonoBehaviour
    {
        public event Action<Creature, Vector2> Hit;

        [SerializeField] private ColliderEventProducer colliderEventProducer;

        public float Speed { get; set; }
        public float Damage { get; set; }

        private Vector2 _direction;
        private bool _isLaunched = false;
        private Creature _attacker;


        public void Launch(AttackContext ctx)
        {
            _direction = ctx.Direction;
            _attacker = ctx.Attacker;

            colliderEventProducer.TriggerEnter += OnProjectileCollision;

            _isLaunched = true;
        }

        private void OnProjectileCollision(Collider2D other)
        {
            if (Creature.IsCreature(other.gameObject) == false)
                return;

            var creature = other.GetComponent<Creature>();

            if (creature == null)
            {
                Debug.LogError("Projectile hit something that is not a creature");
                return;
            }

            if (creature == _attacker)
                return;

            try
            {
                Hit?.Invoke(creature, _direction);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            
            Destroy(gameObject);
        }

        private void Update()
        {
            if (!_isLaunched)
                return;

            transform.position += (Vector3)(_direction * (Speed * Time.deltaTime));
        }
    }
}