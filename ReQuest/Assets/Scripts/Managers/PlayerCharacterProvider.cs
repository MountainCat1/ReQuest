using System;
using UnityEngine;

namespace Managers
{
    public interface IPlayerCharacterProvider
    {
        PlayerCharacter Get();
    }

    public class PlayerCharacterProvider : MonoBehaviour, IPlayerCharacterProvider
    {
        private PlayerCharacter _playerCharacter;

        private void Awake()
        {
            _playerCharacter = FindObjectOfType<PlayerCharacter>() ?? throw new NullReferenceException("PlayerCharacter not found");
        }
        
        public PlayerCharacter Get()
        {
            return _playerCharacter;
        }
    }
}