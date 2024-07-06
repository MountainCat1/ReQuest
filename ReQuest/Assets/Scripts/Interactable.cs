using System.Collections.Generic;
using Managers;
using UnityEngine;
using Zenject;

public class Interactable : MonoBehaviour
{
    [SerializeField] private bool fireOnce = true;
    [SerializeField] private float range = 1.5f;
    [SerializeField] private List<ScriptableAction> actions;
    
    private bool _hasFired;

    [Inject] private IPlayerCharacterProvider _playerProvider;

    private void Update()
    {
        var player = _playerProvider.GetPlayerCharacter();
        
        if (Vector3.Distance(player.transform.position, transform.position) <= range)
        {
            if (!_hasFired)
            {
                Interact();
                _hasFired = fireOnce;
            }
        }
    }

    private void Interact()
    {
        OnInteract();
        foreach (var scriptableAction in actions)
        {
            scriptableAction.Execute();
        }
    }
    
    protected virtual void OnInteract()
    {
        Debug.Log("Interacted with " + name);
    }
}
