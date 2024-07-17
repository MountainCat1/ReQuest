using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class InventoryEntryDisplay : MonoBehaviour
    {
        [Inject] private IPlayerCharacterProvider _playerProvider;
        
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI name;
        
        private InventoryItem _item;
        
        
        public void Initialize(InventoryItem item)
        {
            icon.sprite = item.Icon;
            name.text = item.Name;
            _item = item;
        }

        public void UseItem()
        {
            _item.Use(new IteamUseContext()
            {
                Creature = _playerProvider.Get()
            });
        }
    }
}