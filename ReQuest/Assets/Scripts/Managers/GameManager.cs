using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Items;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [Inject] IPlayerCharacterProvider _playerCharacterProvider;
        [Inject] IPhaseManager _phaseManager;
        
        [SerializeField] private List<ItemBehaviour> startingItems;
        
        private void Start()
        {
            _phaseManager.PhaseChanged += OnTimeRunOut;
            _playerCharacterProvider.Get().Death += OnPlayerDeath;
            
            InitializePlayer();  
        }   

        private void OnPlayerDeath(DeathContext ctx)
        {
            Task.Run(async () =>
            {
                await Task.Delay(5000);
                Application.Quit();
            });
        }

        private void OnTimeRunOut(int phase)
        {
            if(phase != IPhaseManager.EndPhase)
                return;
            
            Task.Run(async () =>
            {
                await Task.Delay(5000);
                Application.Quit();
            });
        }

        private void InitializePlayer()
        {
            var player = _playerCharacterProvider.Get();

            foreach (var startingItem in startingItems)
            {
                player.Inventory.AddItem(startingItem);
            }
            
            var startingWeapon = player.Inventory.Items.FirstOrDefault(x => x is Weapon) as Weapon;
            if (!startingWeapon)
            {
                Debug.LogError("No starting weapon found");
                return;
            }
            startingWeapon.Use(new ItemUseContext()
            {
                Creature = player
            });
        }
    }
}