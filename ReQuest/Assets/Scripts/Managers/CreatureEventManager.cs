using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class CreatureEventManager : MonoBehaviour
    {
        [Inject] private ICreatureManager _creatureManager;
        [Inject] private IPopupManager _popupManager;

        [Inject] 
        private void Construct()
        {
            _creatureManager.CreatureSpawned += OnCreatureSpawned;
        }

        private void OnCreatureSpawned(Creature creature)
        {
            RegisterCreatureEvents(creature);   
        }
        
        private void RegisterCreatureEvents(Creature creature)
        {
           creature.Death += OnCreatureDeath;
        }

        
        // Event Handlers
        private void OnCreatureDeath(DeathContext ctx)
        {
            var creature = ctx.Creature;
            var killer = ctx.Killer;

            killer.AwardXp(creature.XpAmount);
            _popupManager.SpawnFloatingText($"+{creature.XpAmount}", creature.transform.position, color: Color.yellow);
            
            Debug.Log($"{creature.name} was killed by {killer.name}");
        }
    }
}