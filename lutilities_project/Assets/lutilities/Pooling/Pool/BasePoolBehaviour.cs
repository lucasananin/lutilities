using UnityEngine;

namespace lutilities.Pooling
{
    public abstract class BasePoolBehaviour<T> : MonoBehaviour where T : Component
    {
        [SerializeField] BasePoolDataSO<T> _dataSO = null;

        private readonly PoolModel<T> _model = new();

        private void Awake()
        {
            _model.InitData<T>(_dataSO);
            _model.Initialize(transform);
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
