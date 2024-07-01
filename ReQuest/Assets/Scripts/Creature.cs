using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Creature : MonoBehaviour
{
    public event Action Death;

    public Rigidbody2D Rigidbody2D => _rigidbody2D;

    [field: Header("Movement")]
    [field: SerializeField]
    public float Drag { get; private set; }

    [field: SerializeField]
    public float Speed { get; private set; }

    [field: SerializeField]
    public Weapon DefaultWeapon { get; private set; }

    [SerializeField]
    private ModifiableValue health;

    public IReadonlyModifiableValue Health => health;

    private Transform _rootTransform;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _moveDirection;
    private Vector2 _momentum;
    private const float MomentumLoss = 2f;

    private void Awake()
    {
        _rootTransform = transform;
        _rigidbody2D = GetComponent<Rigidbody2D>();

        health.ValueChanged += OnHealthChanged;
        health.CurrentValue = health.MaxValue;
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
        _momentum -= _momentum * (MomentumLoss * Time.fixedDeltaTime);
        if (_momentum.magnitude < 0.1f)
            _momentum = Vector2.zero;

        var change = Vector2.MoveTowards(_rigidbody2D.velocity, _moveDirection * Speed + _momentum, Drag * Time.fixedDeltaTime);
        _rigidbody2D.velocity = change;
    }

    public ICollection<Creature> GetAllVisibleCreatures()
    {
        return FindObjectsOfType<Creature>();
    }

    public Attitude GetAttitudeTowards(Creature other)
    {
        return other == this ? Attitude.Friendly : Attitude.Hostile;
    }

    public void Damage(float damage)
    {
        health.CurrentValue -= damage;
    }

    public void Damage(float damage, Vector2 push)
    {
        health.CurrentValue -= damage;
        Push(push);
    }

    private void Push(Vector2 push)
    {
        _momentum = push;
    }

    public static bool IsCreature(Component go)
    {
        return go.CompareTag("Player") || go.CompareTag("Creature");
    }
}

public enum Attitude
{
    Friendly,
    Neutral,
    Hostile
}
