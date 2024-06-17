using UnityEngine;

[RequireComponent(typeof(Creature))]
public class CreatureController : MonoBehaviour
{
    protected Creature Creature;
    
    private void Awake()
    {
        Creature = GetComponent<Creature>();
    }
    
}