using System;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Managers
{
    public interface IPlayerCharacterProvider
    {
        PlayerCharacter Get();
        bool IsPlayer(Object other);
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

        public bool IsPlayer(Object other)
        {
            if (other is Collider2D collider)
            {
                return collider.gameObject == _playerCharacter.gameObject;
            }
            
            if(other is GameObject gameObject)
            {
                return gameObject == _playerCharacter.gameObject;
            }
            
            if(other is Component component)
            {
                return component == _playerCharacter;
            }

            return false;
        }
    }
}