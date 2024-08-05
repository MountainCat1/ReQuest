using System.Collections.Generic;
using ScriptableActions.Conditions;
using UnityEngine;

namespace Triggers
{
    public class TriggerBase : MonoBehaviour
    {
        [field: SerializeField] public List<ScriptableAction> Actions { get; private set; }
        [field: SerializeField] public List<ConditionBase> Conditions { get; private set; }
        [field: SerializeField] public bool FireOnce { get; private set; } = true;
        protected bool CanRun => !(FireOnce && HasFired);
        
        protected bool HasFired { get; private set; }
        
        protected void RunActions()
        {
            if (FireOnce && HasFired)
                return;
         
            if(CheckConditions())
                return;
            
            HasFired = true;
            
            foreach (var action in Actions)
            {
                action.Execute();
            }
        }

        private bool CheckConditions()
        {
            foreach (var condition in Conditions)
            {
                if (!condition.Check())
                    return true;
            }

            return false;
        }
    }
}