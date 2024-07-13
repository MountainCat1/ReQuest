using System;
using System.Collections.Generic;

public class Inventory
{
    public Action OnChange { get; set; }
    
    public IReadOnlyList<InventoryItem> Items => _items;
    private readonly List<InventoryItem> _items = new();

    public void AddItem(InventoryItem item)
    {
        _items.Add(item);
        OnChange?.Invoke();
    }
    
    public void RemoveItem(InventoryItem item)
    {
        _items.Remove(item);
        OnChange?.Invoke();
    }
}