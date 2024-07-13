using System.Collections.Generic;
using UnityEngine;

public enum Teams
{
    Passive,
    Player,
    Kingdom,
    Villagers,
    Kobolds
}

public enum Attitude
{
    Friendly,
    Neutral,
    Hostile
}

namespace Managers
{
    interface ITeamManager
    {
        Attitude GetAttitude(Teams team1, Teams team2);
    }
    
    public class TeamManager : MonoBehaviour, ITeamManager
    {
        // Public Constants

        // Static Variables and Methods

        // Public Variables

        // Serialized Private Variables

        // Injected Dependencies (using Zenject)

        // Private Variables
        private readonly Dictionary<Teams, Dictionary<Teams, Attitude>> _relations = new();
        // Properties

        // Events

        // Unity Callbacks
        private void Start()
        {
            // First set all relations to neutral
            foreach (Teams team1 in System.Enum.GetValues(typeof(Teams)))
            {
                _relations[team1] = new Dictionary<Teams, Attitude>();
                foreach (Teams team2 in System.Enum.GetValues(typeof(Teams)))
                {
                    _relations[team1][team2] = Attitude.Neutral;
                }
            }
            
            // Player
            AddRelation(Teams.Player, Teams.Villagers, Attitude.Friendly);
            
            // Villagers
            AddRelation(Teams.Villagers, Teams.Player, Attitude.Friendly);
            
            // Kobolds
            AddRelation(Teams.Kobolds, Teams.Player, Attitude.Hostile);
            AddRelation(Teams.Kobolds, Teams.Kingdom, Attitude.Hostile);
            AddRelation(Teams.Kobolds, Teams.Villagers, Attitude.Hostile);
        }


        // Public Methods
        public Attitude GetAttitude(Teams team1, Teams team2)
        {
            return _relations[team1][team2];
        }

        // Virtual Methods

        // Abstract Methods

        // Private Methods
        private void AddRelation(Teams team1, Teams team2, Attitude attitude)
        {
            _relations[team1][team2] = attitude;
            _relations[team2][team1] = attitude;
        }
        
        // Event Handlers
    }
}