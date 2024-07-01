﻿using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Creature playerCreature;
    [SerializeField] private Weapon playerWeapon;
    
    [Inject] private IInputManager _inputManager;
    [Inject] private IInputMapper _inputMapper;
    
    private void Start()
    {
        _inputManager.CharacterMovementChanged += OnCharacterMovementChange;
        _inputMapper.OnPlayerCharacterAttack += OnCharacterAttack;
    }

    private void OnCharacterAttack(AttackContext ctx)
    {
        playerWeapon.ContiniousAttack(ctx);
    }

    private void OnCharacterMovementChange(Vector2 move)
    {
        playerCreature.SetMovement(move);
    }
}