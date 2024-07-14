using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class Creature : MonoBehaviour
{
    // Events
    public event Action Death;

    // Injected Dependencies (using Zenject)
    [Inject] private ITeamManager _teamManager;

    // Public Variables
    public Rigidbody2D Rigidbody2D => _rigidbody2D;
    public Inventory Inventory { get; } = new();
    public IReadonlyModifiableValue Health => health;

    // Serialized Private Variables
    [field: Header("Movement")]
    [field: SerializeField]
    public float Drag { get; private set; }

    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public Weapon DefaultWeapon { get; private set; }

    [field: Header("Stats")] [field: SerializeField]
    private ModifiableValue health;

    [field: SerializeField] public float XpAmount { get; private set; }

    [SerializeField] private Teams team;

    // Private Variables
    private Transform _rootTransform;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _moveDirection;
    private Vector2 _momentum;
    private const float MomentumLoss = 2f;
    private Creature _lastAttackedBy = null;

    // Properties

    // Unity Callbacks
    private void Awake()
    {
        _rootTransform = transform;
        _rigidbody2D = GetComponent<Rigidbody2D>();

        health.ValueChanged += OnHealthChanged;
        health.CurrentValue = health.MaxValue;
    }

    private void FixedUpdate()
    {
        UpdateVelocity();
    }


    // Public Methods
    public void SetMovement(Vector2 direction)
    {
        _moveDirection = direction;
    }

    public ICollection<Creature> GetAllVisibleCreatures()
    {
        return FindObjectsOfType<Creature>();
    }

    public Attitude GetAttitudeTowards(Creature other)
    {
        if (other == null)
            throw new ArgumentNullException(nameof(other));

        if (other == this)
            return Attitude.Friendly;

        return _teamManager.GetAttitude(team, other.team);
    }

    public void Damage(HitContext ctx)
    {
        health.CurrentValue -= ctx.Damage;
        _lastAttackedBy = ctx.Attacker;
        
        if (ctx.PushForce.magnitude > 0)
            Push(ctx.PushForce);
    }

    // Virtual Methods

    // Abstract Methods

    // Private Methods
    private void UpdateVelocity()
    {
        _momentum -= _momentum * (MomentumLoss * Time.fixedDeltaTime);
        if (_momentum.magnitude < 0.1f)
            _momentum = Vector2.zero;

        var change = Vector2.MoveTowards(_rigidbody2D.velocity, _moveDirection * Speed + _momentum,
            Drag * Time.fixedDeltaTime);
        _rigidbody2D.velocity = change;
    }

    private void Push(Vector2 push)
    {
        _momentum = push;
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

    public static bool IsCreature(Component go)
    {
        return go.CompareTag("Player") || go.CompareTag("Creature");
    }


    // Event Handlers

    protected virtual void OnHealthChanged()
    {
        if (health.CurrentValue <= health.MinValue)
            InvokeDeath();
    }
}