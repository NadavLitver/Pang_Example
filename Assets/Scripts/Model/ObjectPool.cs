using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace model
{
    public class ObjectPool<T> : IObjectPool<T> where T : Component
    {
        private readonly T prefabRef;
        private readonly DiContainer container;
        private int poolSize = 30;
        private List<T> pool = new List<T>();
        public List<T> Pool => pool;

      

        [Inject]
        public ObjectPool(T _prefabRef, DiContainer _container)
        {
            prefabRef = _prefabRef;
            container = _container;
            PopulatePool();
        }

        public void PopulatePool()
        {
            for (int i = 0; i < poolSize; i++)
            {
                T current = container.InstantiatePrefabForComponent<T>(prefabRef);
                current.name = prefabRef.name + i;
                pool.Add(current);
                current.gameObject.SetActive(false);
            }
        }

        public T GetFromPool()
        {
            for (int i = 0; i < pool.Count; i++)
            {
                if (!pool[i].gameObject.activeInHierarchy)
                {
                    pool[i].gameObject.SetActive(true);
                    return pool[i];
                }
            }

            T current = container.InstantiatePrefabForComponent<T>(prefabRef);
            current.name = prefabRef.name + "Extra";
            pool.Add(current);
            current.gameObject.SetActive(true);
            return current;
        }

        public void ReturnToPool(T obj)
        {
            obj.gameObject.SetActive(false);
        }

       
    }

}
