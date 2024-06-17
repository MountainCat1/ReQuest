using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Creature : MonoBehaviour
{
    public event Action Death;

    public Rigidbody2D Rigidbody2D => _rigidbody2D;

    [field: Header("Movement")]
    [field: SerializeField]
    public float Drag { get; private set; }

    [field: SerializeField] public float Speed { get; private set; }
    [SerializeField] private ModifiableValue health;

    public IReadonlyModifiableValue Health => health;

    private Transform _rootTransform;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _moveDirection;


    private void Awake()
    {
        _rootTransform = transform;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected void Start()
    {
        health.ValueChanged += OnHealthChanged;
    }

    protected virtual void OnHealthChanged()
    {
        if (health.CurrentValue <= health.MinValue)
            InvokeDeath();
    }

    private void InvokeDeath()
    {
        try
        {
            Death?.Invoke();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            throw;
        }

        Destroy(gameObject);
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