using UnityEngine;
using UnityEngine.Events;

namespace lutilities.EventSO
{
    public abstract class EventSO1<T> : ScriptableObject
    {
        private event UnityAction<T> OnEventRaised;

        public void Invoke(T value)
        {
            OnEventRaised?.Invoke(value);
        }

        public void Subscribe(UnityAction<T> listener)
        {
            OnEventRaised += listener;
        }

        public void Unsubscribe(UnityAction<T> listener)
        {
            OnEventRaised -= listener;
        }

        public void UnsubscribeAll()
        {
            OnEventRaised = null;
        }
    }
}
