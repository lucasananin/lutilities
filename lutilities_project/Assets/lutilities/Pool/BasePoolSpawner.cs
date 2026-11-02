using UnityEngine;

namespace lutilities.Pooling
{
    public abstract class BasePoolSpawner<T, TPool> : MonoBehaviour
        where T : Component
        where TPool : BasePoolSO<T>
    {
        [SerializeField] private TPool _poolSO;

        protected virtual void Awake()
        {
            _poolSO.Initialize(transform);
        }

        public T GetObject()
        {
            return _poolSO.Get();
        }

        public void ReturnObject(T obj)
        {
            _poolSO.Return(obj);
        }
    }
}
