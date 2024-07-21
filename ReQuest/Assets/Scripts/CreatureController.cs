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
        var distance = Vector2.Distance(transform.position, target.transform.position); 
        
        // If the target is too far away, we can't see it
        if (distance > 100f)
            return false;


        var layerMask = LayerMask.GetMask("Walls");
        var hit = Physics2D.Raycast(
            Creature.transform.position, target.transform.position - Creature.transform.position,
            distance,
            layerMask
        );
        return !hit;
    }
}