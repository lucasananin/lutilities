using UnityEngine;
using UnityEngine.Events;

namespace lutilities.DataTypeSO
{
    public abstract class GenericDataTypeSO<T> : ScriptableObject
    {
        [SerializeField] protected T _currentValue;

        public UnityAction<T> OnChanged = null;

        public T CurrentValue
        {
            get => _currentValue;
            set => _currentValue = value;
        }

        public void Set(T newValue)
        {
            _currentValue = newValue;
            OnChanged?.Invoke(_currentValue);
        }

        public T Get()
        {
            return _currentValue;
        }
    }
}