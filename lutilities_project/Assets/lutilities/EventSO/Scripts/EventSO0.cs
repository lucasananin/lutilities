using UnityEngine;
using UnityEngine.Events;

namespace lutilities.EventSO
{
    public abstract class EventSO0 : ScriptableObject
    {
        private event UnityAction OnEventRaised;

        public void Invoke()
        {
            OnEventRaised?.Invoke();
        }

        public void Subscribe(UnityAction listener)
        {
            OnEventRaised += listener;
        }

        public void Unsubscribe(UnityAction listener)
        {
            OnEventRaised -= listener;
        }

        public void UnsubscribeAll()
        {
            OnEventRaised = null;
        }
    }
}
