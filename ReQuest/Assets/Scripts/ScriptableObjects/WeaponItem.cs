using Managers;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Custom/InventoryItemWeapon")]
public class WeaponItem : InventoryItem
{
     [Inject] private IPlayerCharacterProvider _playerProvider;
     
     [field: SerializeField] public Weapon WeaponPrefab { get; private set; }


     public override void Use(IteamUseContext ctx)
     {
          base.Use(ctx);
          
          ctx.Creature.SetWeapon(this);
     }
}