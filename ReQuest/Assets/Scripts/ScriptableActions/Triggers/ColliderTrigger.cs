using Managers;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Triggers
{
    public class ColliderTrigger : TriggerBase
    {
        [SerializeField] private ColliderEventProducer colliderEventProducer;
        
        [Inject] IPlayerCharacterProvider _playerProvider;

        protected override void Start()
        {
            base.Start();
            
            colliderEventProducer.TriggerEnter += HandleTriggerEnter;
        }

        private void HandleTriggerEnter(Collider2D other)
        {
            if (!_playerProvider.IsPlayer(other))
                return;

            RunActions();
        }
    }
}