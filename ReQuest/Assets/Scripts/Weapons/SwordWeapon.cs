﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SwordWeapon : Weapon
{
    [SerializeField] private Transform swordParent;
    [SerializeField] private Collider2D swordCollider;
    [SerializeField] private GameObject swordVisual;
    [SerializeField] private float attackAngle = 90f;
    [SerializeField] private float swingSpeed = 90f;
    [SerializeField] private ColliderEventProducer colliderEventProducer;
    
    private Coroutine _attackCoroutine;
    private List<Creature> _hitCreatures = new List<Creature>();

    private void Awake()
    {
        swordCollider.enabled = false;
        swordVisual.SetActive(false);
        
        colliderEventProducer.TriggerStay += OnSwordCollisionStay;
    }

    protected override void Attack(AttackContext ctx)
    {
        // Rotate sword parent to the starting position
        swordParent.rotation =
            Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, ctx.Direction) - attackAngle / 2);

        if (_attackCoroutine != null)
            StopCoroutine(_attackCoroutine);
        
        _attackCoroutine = StartCoroutine(AttackRoutine(ctx));
    }

    private void OnSwordHit(Creature creature)
    {
        creature.Damage(Damage, CalculatePushForce(creature));
    }

    private IEnumerator AttackRoutine(AttackContext ctx)
    {
        swordCollider.enabled = true;
        swordVisual.SetActive(true);
        _hitCreatures.Clear();

        // Rotate partent to simulate a swing
        var startRotation = swordParent.rotation;
        var targetRotation =
            Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, ctx.Direction) + attackAngle / 2);
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime * swingSpeed / attackAngle;
            swordParent.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            yield return null;
        }

        swordCollider.enabled = false;
        swordVisual.SetActive(false);
        _hitCreatures.Clear();
    }
    
    private void OnSwordCollisionStay(Collider2D collision)
    {
        if(!Creature.IsCreature(collision))
            return;
        
        var creature = collision.GetComponent<Creature>();
        if(_hitCreatures.Contains(creature))
            return;
        
        _hitCreatures.Add(creature);
        OnSwordHit(creature);
    }
}