using UnityEngine;
using UnityEngine.Events;

namespace lutilities.EventSO
{
    public abstract class EventSO2<T1, T2> : ScriptableObject
    {
        private event UnityAction<T1, T2> OnEventRaised;

        public void Invoke(T1 arg1, T2 arg2)
        {
            OnEventRaised?.Invoke(arg1, arg2);
        }

        public void Subscribe(UnityAction<T1, T2> listener)
        {
            OnEventRaised += listener;
        }

        public void Unsubscribe(UnityAction<T1, T2> listener)
        {
            OnEventRaised -= listener;
        }

        public void UnsubscribeAll()
        {
            OnEventRaised = null;
        }
    }
}
