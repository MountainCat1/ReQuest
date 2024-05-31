using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Creature : MonoBehaviour
{
    public Rigidbody2D Rigidbody2D => _rigidbody2D;
    
    [field: Header("Movement")]
    [field: SerializeField] public float Drag { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    
    private Transform _rootTransform;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _moveDirection;
    
    private void Awake()
    {
        _rootTransform = transform;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    public void SetMovement(Vector2 direction)
    {
        _moveDirection = direction;
    }
    
    private void FixedUpdate()
    {
        UpdateVelocity();
    }
    
    private void UpdateVelocity()
    {
        var change = Vector2.MoveTowards(_rigidbody2D.velocity, _moveDirection * Speed, Drag * Time.fixedDeltaTime);
        _rigidbody2D.velocity = change;
    }
}