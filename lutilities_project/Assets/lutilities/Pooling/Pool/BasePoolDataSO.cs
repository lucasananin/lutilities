using UnityEngine;

namespace lutilities.Pooling
{
    public abstract class BasePoolDataSO<T> : ScriptableObject where T : Component
    {
        [SerializeField] private T _prefab;
        [SerializeField] private int _initialSize = 10;

        public T Prefab { get => _prefab; }
        public int InitialSize { get => _initialSize; }
    }
}
