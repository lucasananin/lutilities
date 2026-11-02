using System.Collections.Generic;
using UnityEngine;

namespace lutilities.Pooling
{
    public abstract class BasePoolSO<T> : ScriptableObject where T : Component
    {
        [Header("Pool Settings")]
        [SerializeField] private T _prefab;
        [SerializeField] private int _initialSize = 10;

        private readonly List<T> _pool = new();
        private readonly List<T> _activeObjects = new();
        private Transform _poolParent;

        private void OnEnable()
        {
            _pool.Clear();
            _activeObjects.Clear();
        }

        public void Initialize(Transform poolParent)
        {
            _poolParent = poolParent;

            for (int i = 0; i < _initialSize; i++)
            {
                CreateObject();
            }
        }

        public T Get()
        {
            int _count = _pool.Count;

            for (int i = 0; i < _count; i++)
            {
                T obj = _pool[i];
                if (!obj.gameObject.activeInHierarchy)
                {
                    obj.gameObject.SetActive(true);
                    _activeObjects.Add(obj);

                    return obj;
                }
            }

            T newObject = CreateObject(true);
            _activeObjects.Add(newObject);

            return newObject;
        }

        public void Return(T obj)
        {
            if (!_activeObjects.Remove(obj)) return;

            obj.gameObject.SetActive(false);
            obj.transform.SetParent(_poolParent);
        }

        public void ReturnAll()
        {
            int _count = _activeObjects.Count;
            for (int i = _count - 1; i >= 0; i--)
            {
                Return(_activeObjects[i]);
            }
        }

        private T CreateObject(bool activate = false)
        {
            T obj = Instantiate(_prefab, _poolParent);

            obj.gameObject.SetActive(activate);

            _pool.Add(obj);

            return obj;
        }
    }
}
