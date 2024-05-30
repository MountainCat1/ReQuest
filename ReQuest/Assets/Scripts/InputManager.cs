using System;
using UnityEngine;


public interface IInputManager
{
    event Action<Vector2> OnCharacterMovement;
}

public class InputManager : MonoBehaviour, IInputManager
{
    public event Action<Vector2> OnCharacterMovement;
    
    private void Awake()
    {
        var inputActions = new InputActions();
        inputActions.Enable();
        
        inputActions.CharacterControl.Movement.performed +=
            ctx => OnCharacterMovement?.Invoke(ctx.ReadValue<Vector2>());
    }
}