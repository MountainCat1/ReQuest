using System.Collections.Generic;
using Managers;
using UnityEngine;
using Zenject;

namespace Triggers
{
    public class InteractableTriggerBase : TriggerBase
    {
        [SerializeField] private float range = 1.5f;
    
        [Inject] private IPlayerCharacterProvider _playerProvider;

        private void Update()
        {
            var player = _playerProvider.Get();

            if (Vector3.Distance(player.transform.position, transform.position) <= range)
            {
                RunActions();
            }
        }
    }

}