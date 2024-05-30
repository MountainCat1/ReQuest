using UnityEngine;

public class Creature : MonoBehaviour
{
    [field: SerializeField] public float Speed { get; private set; }
    
    private Transform _rootTransform;
    
    private void Awake()
    {
        _rootTransform = transform;
    }
    
    public void Move(Vector2 direction)
    {
        _rootTransform.Translate(direction * Speed * Time.deltaTime);
    }
}