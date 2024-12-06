using UnityEngine;
using Zenject;

namespace _Elementa.ObjectPool
{
    public class PoolsInstaller : MonoInstaller
    {
        [SerializeField] private Enemy enemyPrefab;

        [SerializeField] private int poolSize = 10;

        public override void InstallBindings()
        {
            BindPool(enemyPrefab, PoolIds.Enemies);
        }

        private void BindPool<T>(T prefab, string id) where T : MonoBehaviour
        {
            Container.Bind<ObjectPool<T>>().WithId(id).FromMethod(context =>
            {
                var parent = new GameObject(id).transform;
                var pool = new ObjectPool<T>(prefab, poolSize, context.Container, parent);
                pool.Initialize();
                return pool;
            }).AsCached().NonLazy();
        }
    }
}