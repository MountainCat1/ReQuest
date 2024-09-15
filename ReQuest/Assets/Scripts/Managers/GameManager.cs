using System.Collections.Generic;
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
            InitializePlayer();  
            _phaseManager.PhaseChanged += OnTimeRunOut;
            _playerCharacterProvider.Get().Death += OnPlayerDeath;
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
            foreach (var item in startingItems)
            {
                player.Inventory.AddItem(item);
            }
        }
    }
}