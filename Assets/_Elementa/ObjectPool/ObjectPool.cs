using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Elementa.ObjectPool
{
    public class ObjectPool<T> : IInitializable where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly Transform _poolParentFold;
        private readonly Queue<T> _pool = new();
        private readonly int _initialSize;
        private readonly DiContainer _diContainer;

        public ObjectPool(T prefab, int initialSize, DiContainer diContainer, Transform poolParent = null)
        {
            _prefab = prefab;
            _initialSize = initialSize;
            _poolParentFold = poolParent;
            _diContainer = diContainer;
        }

        public void Initialize()
        {
            for (int i = 0; i < _initialSize; i++)
            {
                AddObjectToPool();
            }
        }

        private T AddObjectToPool()
        {
            var newObj = _diContainer.InstantiatePrefab(_prefab, _poolParentFold).GetComponent<T>();
            newObj.gameObject.SetActive(false);
            _pool.Enqueue(newObj);
            return newObj;
        }

        public T Get()
        {
            if (_pool.Count == 0)
            {
                AddObjectToPool();
            }

            var obj = _pool.Dequeue();
            obj.gameObject.SetActive(true); 
            return obj;
        }

        public T OnlyGet()
        {
            if (_pool.Count == 0)
            {
                AddObjectToPool();
            }

            var obj = _pool.Dequeue();
            return obj;
        }
        
        public void ReturnToPool(T obj)
        {
            obj.gameObject.SetActive(false);
            obj.gameObject.transform.parent = _poolParentFold;
            _pool.Enqueue(obj);
        }
    }
}