using System;
using System.Linq;
using Items;
using Managers;
using UnityEngine;
using Zenject;

namespace ScriptableActions.Conditions
{
    public class PlayerHasItemCondition : ConditionBase
    {
        [Inject] IPlayerCharacterProvider _playerProvider;

        [SerializeField] private ItemBehaviour item;

        private void Start()
        {
            if(item == null)
                Debug.LogError("Item is not set in PlayerHasItemCondition");
        }

        public override bool Check()
        {
            var player = _playerProvider.Get();

            Debug.Log($"Checking if player has item {item.GetIdentifier()}");

            return player.Inventory.Items.Any(x => x.GetIdentifier().Equals(item.GetIdentifier()));
        }
    }
}