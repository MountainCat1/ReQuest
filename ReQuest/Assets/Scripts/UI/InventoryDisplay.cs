using Managers;
using UnityEngine;
using Zenject;

namespace UI
{
    public class InventoryDisplay : MonoBehaviour
    {
        [Inject] IPlayerCharacterProvider _playerCharacterProvider;
        [Inject] private DiContainer _container;
        
        [SerializeField] private InventoryEntryDisplay inventoryEntryDisplayPrefab;
        [SerializeField] private Transform inventoryParent;
        
        
        private Creature _creature;
        
        private void Start()
        {
            _creature = _playerCharacterProvider.Get();
            _creature.Inventory.OnChange += UpdateInventory;
            UpdateInventory();
        }

        private void UpdateInventory()
        {
            foreach (Transform child in inventoryParent)
            {
                Destroy(child.gameObject);
            }

            foreach (var item in _creature.Inventory.Items)
            {
                var entry = Instantiate(inventoryEntryDisplayPrefab, inventoryParent);
                _container.Inject(entry);
                entry.Initialize(item);
            }                        
        }
    }
}