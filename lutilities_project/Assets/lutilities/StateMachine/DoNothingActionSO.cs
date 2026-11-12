using UnityEngine;

namespace lutilities.StateMachine.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Action_DoNothing", menuName = "Scriptable Objects/State Machines/Actions/Do Nothing")]
    public class DoNothingActionSO : StateActionSO<DoNothingAction>
    {
        //
    }

    public class DoNothingAction : StateAction
    {
        public override void OnFixedUpdate()
        {
            //
        }

        public override void OnUpdate()
        {
            //
        }
    }
}
