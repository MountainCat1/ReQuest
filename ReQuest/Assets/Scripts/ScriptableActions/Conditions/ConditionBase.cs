using UnityEngine;

namespace ScriptableActions.Conditions
{
    public abstract class ConditionBase : MonoBehaviour
    {
        public abstract bool Check();
    }
}