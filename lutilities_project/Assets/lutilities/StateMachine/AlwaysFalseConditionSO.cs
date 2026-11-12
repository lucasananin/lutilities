using UnityEngine;

namespace lutilities.StateMachine.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Condition_AlwaysFalse", menuName = "Scriptable Objects/State Machines/Conditions/Always False")]
    public class AlwaysFalseConditionSO : StateConditionSO<AlwaysFalseCondition>
    {
        //
    }

    public class AlwaysFalseCondition : Condition
    {
        protected override bool Statement()
        {
            return false;
        }
    }
}
