﻿using System;
using Managers;
using UnityEngine;
using Zenject;

namespace ScriptableActions
{
    public class GiveItemAction : ScriptableAction
    {
        [Inject] IPlayerCharacterProvider _playerProvider;
        
        [SerializeField] private InventoryItem item;
        
        public override void Execute()
        {
            base.Execute();

            _playerProvider.Get().Inventory.AddItem(item);
        }
    }
}