using System;
using System.Collections.Generic;
using Items;
using UnityEngine;
using Zenject;

public class Inventory
{
    public Action OnChange { get; set; }

    [Inject] private DiContainer _diContainer;
    
    public IReadOnlyList<ItemBehaviour> Items => _items;
    private readonly List<ItemBehaviour> _items = new();

    private Transform _transform;
    
    public Inventory(Transform rootTransform)
    {
        _transform = rootTransform;
    }    
    
    public void AddItem(ItemBehaviour itemPrefab)
    {
        var instantiateItem = _diContainer.InstantiatePrefab(
            itemPrefab,
            _transform.position,
            Quaternion.identity,
            _transform
        );
        var itemScript = instantiateItem.GetComponent<ItemBehaviour>();
        _items.Add(itemScript);
        
        OnChange?.Invoke();
    }

    // public void RemoveItem(InventoryItem item)
    // {
    //     _items.Remove(item);
    //     OnChange?.Invoke();
    // }
    public void RemoveItem(ItemBehaviour item)
    {
        _items.Remove(item);
        OnChange?.Invoke();
    }
    
    public ItemBehaviour GetItem(string identifier)
    {
        return _items.Find(x => x.GetIdentifier().Equals(identifier));
    }
}