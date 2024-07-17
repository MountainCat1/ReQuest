using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Custom/InventoryItem")]
public class InventoryItem : ScriptableObject
{
    [field: SerializeField] public Sprite Icon { get; set; }
    [field: SerializeField] public string Name { get; set; }
    
    public virtual void Use(IteamUseContext ctx)
    {
        var creature = ctx.Creature;
        if(creature == null)
        {
            Debug.LogError("Creature is null");
            return;
        }
        
        Debug.Log($"{creature} using item " + Name);
    }
}

public class IteamUseContext
{
    public Creature Creature { get; set; }
}