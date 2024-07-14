using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class CreatureEventManager : MonoBehaviour
    {
        [Inject] private CreatureManager _creatureManager;

        [Inject] 
        private void Construct()
        {
            _creatureManager.CreatureSpawned += OnCreatureSpawned;
        }

        private readonly Dictionary<Creature, IList<Action>> _creatureDeathHandlers = new();

        private void OnCreatureSpawned(Creature creature)
        {
            RegisterCreatureEvents(creature);   
        }
        
        private void RegisterCreatureEvents(Creature creature)
        {
            _creatureDeathHandlers[creature] = new List<Action>();
            
           creature.Death += () => OnCreatureDeath(creature);
        }

        private void OnCreatureDeath(Creature creature)
        {
        }
    }
}