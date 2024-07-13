using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "InventoryItem")]
public class InventoryItem : ScriptableObject
{
    [field: SerializeField] public Sprite Icon { get; set; }
    [field: SerializeField] public string Name { get; set; }
}