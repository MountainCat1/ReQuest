using Managers;
using UnityEngine;
using Zenject;

namespace Triggers
{
    [RequireComponent(typeof(Collider2D))]
    public class ColliderTrigger : TriggerBase
    {
        [Inject] IPlayerCharacterProvider _playerProvider;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_playerProvider.IsPlayer(other))
                return;

            RunActions();
        }
    }
}