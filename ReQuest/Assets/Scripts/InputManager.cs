using System;
using UnityEngine;


public interface IInputManager
{
    event Action<Vector2> CharacterMovement;
}

public class InputManager : MonoBehaviour, IInputManager
{
    public event Action<Vector2> CharacterMovement;
    
    private InputActions _inputActions;
    
    private void Awake()
    {
        _inputActions = new InputActions();
        _inputActions.Enable();
        
        _inputActions.CharacterControl.Movement.performed +=
            ctx => CharacterMovement?.Invoke(ctx.ReadValue<Vector2>());
    }
    
    
    private void Update()
    {
        var movement = _inputActions.CharacterControl.Movement.ReadValue<Vector2>();
        if(movement.magnitude > 0)
            CharacterMovement?.Invoke(movement);
    }
}