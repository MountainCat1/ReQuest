using System;
using UnityEngine;

public interface IInputManager
{
    event Action<Vector2> CharacterMovement;
    event Action<Vector2> CharacterMovementFixed;
    event Action<Vector2> CharacterMovementChanged;
}

public class InputManager : MonoBehaviour, IInputManager
{
    public event Action<Vector2> CharacterMovement;
    public event Action<Vector2> CharacterMovementFixed;
    public event Action<Vector2> CharacterMovementChanged;
    
    private InputActions _inputActions;
    
    private Vector2 _cachedCharacterMovement = Vector2.zero;
    
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
        if (movement.magnitude > 0)
            CharacterMovement?.Invoke(movement);
    }
    
    private void FixedUpdate()
    {
        var movement = _inputActions.CharacterControl.Movement.ReadValue<Vector2>();
        if (movement.magnitude > 0)
            CharacterMovementFixed?.Invoke(movement);
        
        if (_cachedCharacterMovement != movement)
        {
            _cachedCharacterMovement = movement;
            CharacterMovementChanged?.Invoke(movement);
        }
    }
}