using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InventoryEntryDisplay : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI name;
        
        public void Initialize(InventoryItem item)
        {
            icon.sprite = item.Icon;
            name.text = item.Name;
        }
    }
}