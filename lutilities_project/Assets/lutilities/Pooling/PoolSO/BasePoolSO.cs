using UnityEngine;

namespace lutilities.Pooling
{
    public abstract class BasePoolSO<T> : ScriptableObject where T : Component
    {
        [SerializeField] PoolModel<T> _model = null;

        private void OnEnable()
        {
            _model.ClearCollections();
        }

        public void Initialize(Transform poolParent)
        {
            _model.Initialize(poolParent);
        }

        public T Get()
        {
            return _model.Get();
        }

        public void Return(T obj)
        {
            _model.Return(obj);
        }

        public void ReturnAll()
        {
            _model.ReturnAll();
        }
    }
}
