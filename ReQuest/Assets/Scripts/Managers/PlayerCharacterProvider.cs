using System;
using UnityEngine;
using Zenject;

namespace Managers
{
    public interface IPlayerCharacterProvider
    {
        PlayerCharacter Get();
    }

    public class PlayerCharacterProvider : MonoBehaviour, IPlayerCharacterProvider
    {
        private PlayerCharacter _playerCharacter;

        [Inject]
        private void Construct()
        {
            _playerCharacter = FindObjectOfType<PlayerCharacter>() ?? throw new NullReferenceException("PlayerCharacter not found");
        }
        
        public PlayerCharacter Get()
        {
            return _playerCharacter;
        }
    }
}