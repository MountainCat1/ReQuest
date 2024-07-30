using System.Collections.Generic;
using UnityEngine;

namespace Triggers
{
    public class TriggerBase : MonoBehaviour
    {
        [field: SerializeField] public List<ScriptableAction> Actions { get; private set; }
        [field: SerializeField] public bool FireOnce { get; private set; } = true;
        protected bool CanRun => !(FireOnce && HasFired);
        
        protected bool HasFired { get; private set; }
        
        protected void RunActions()
        {
            if (FireOnce && HasFired)
                return;
            
            HasFired = true;
            
            foreach (var action in Actions)
            {
                action.Execute();
            }
        }
    }
}