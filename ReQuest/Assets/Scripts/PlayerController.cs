using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Creature playerCreature;
    
    [Inject] private IInputManager _inputManager;
    
    private void Start()
    {
        _inputManager.CharacterMovement += Move;
    }
    
    private void Move(Vector2 move)
    {
        playerCreature.Move(move);
    }
}