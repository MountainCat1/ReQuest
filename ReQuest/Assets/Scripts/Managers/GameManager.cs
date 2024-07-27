using System.Collections.Generic;
using Items;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [Inject] IPlayerCharacterProvider _playerCharacterProvider;
        
        [SerializeField] private List<ItemBehaviour> startingItems;
        
        private void Start()
        {
            InitializePlayer();  
        }

        private void InitializePlayer()
        {
            var player = _playerCharacterProvider.Get();
            foreach (var item in startingItems)
            {
                player.Inventory.AddItem(item);
            }
        }
    }
}