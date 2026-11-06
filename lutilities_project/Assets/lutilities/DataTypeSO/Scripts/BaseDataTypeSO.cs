using UnityEngine;
using UnityEngine.Events;

namespace lutilities.DataTypeSO
{
    public abstract class BaseDataTypeSO<T> : ScriptableObject
    {
        [SerializeField] protected T _value;

        public UnityAction<T> OnChanged = null;

        public T Value
        {
            get => _value;
            set => _value = value;
        }

        public void Set(T newValue)
        {
            _value = newValue;
            OnChanged?.Invoke(_value);
        }

        public T Get()
        {
            return _value;
        }
    }
}