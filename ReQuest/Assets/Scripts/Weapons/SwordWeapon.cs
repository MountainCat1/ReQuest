using System.Collections;
using UnityEngine;

public class SwordWeapon : Weapon
{
    [SerializeField] private Transform swordParent;
    [SerializeField] private Collider2D swordCollider;
    [SerializeField] private GameObject swordVisual;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private float attackAngle = 90f;

    private Coroutine _attackCoroutine;


    private void Awake()
    {
        swordCollider.enabled = false;
        swordVisual.SetActive(false);
    }

    public override void Attack(AttackContext ctx)
    {
        // Rotate sword parent to the starting position
        swordParent.rotation =
            Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, ctx.Direction) - attackAngle / 2);

        if (_attackCoroutine != null)
            StopCoroutine(_attackCoroutine);
        
        _attackCoroutine = StartCoroutine(AttackRoutine(ctx));
    }


    private IEnumerator AttackRoutine(AttackContext ctx)
    {
        swordCollider.enabled = true;
        swordVisual.SetActive(true);

        // Rotate partent to simulate a swing
        var startRotation = swordParent.rotation;
        var targetRotation =
            Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, ctx.Direction) + attackAngle / 2);
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime * attackSpeed / attackAngle;
            swordParent.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            yield return null;
        }

        swordCollider.enabled = false;
        swordVisual.SetActive(false);
    }
}