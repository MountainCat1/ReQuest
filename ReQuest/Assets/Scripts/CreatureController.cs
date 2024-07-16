using UnityEngine;

[RequireComponent(typeof(Creature))]
public class CreatureController : MonoBehaviour
{
    protected Creature Creature;

    private void Awake()
    {
        Creature = GetComponent<Creature>();
    }

    protected bool CanSee(Creature target)
    {
        // If the target is too far away, we can't see it
        if (Vector2.Distance(transform.position, target.transform.position) > 100f)
            return false;


        var layerMask = LayerMask.GetMask("Walls");
        var hit = Physics2D.Raycast(
            Creature.transform.position, target.transform.position - Creature.transform.position,
            float.PositiveInfinity,
            layerMask
        );
        return !hit;
    }
}