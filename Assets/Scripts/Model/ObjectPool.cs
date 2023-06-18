using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

namespace model
{

    public class ObjectPool : IObjectPool//base class for object pools
    {
        private readonly GameObject prefabRef;
        private readonly DiContainer container;
        private int poolSize = 30;
        private List<GameObject> pool = new List<GameObject>();
        public List<GameObject> Pool => pool;

        [Inject]
        public ObjectPool(GameObject _prefabRef,DiContainer _container)
        {
           prefabRef = _prefabRef;
           container = _container;
           PopulatePool();
        }
        /// <summary>
        /// loop over the pool size and use the container to instantiate the corresponding prefab
        /// using the container instantiation also injects injectables to the prefab
        /// 
        /// </summary>
        public void PopulatePool()
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject current = container.InstantiatePrefab(prefabRef);
                current.name = prefabRef.name + i;
                pool.Add(current);
                current.SetActive(false);
            }
        }
        /// <summary>
        /// get prefab from pool if its inactive, if there are no prefabs left create a new one
        /// </summary>
        /// <returns></returns>
        public GameObject GetFromPool()
        {
            for (int i = 0; i < pool.Count; i++)
            {
                if (!pool[i].activeInHierarchy)
                {
                    pool[i].SetActive(true);
                    return pool[i];
                }
            }

            GameObject current = container.InstantiatePrefab(prefabRef);
            current.name = prefabRef.name + "Extra";
            pool.Add(current);
            current.SetActive(true);
            return current;
        }
        /// <summary>
        /// turn off a prefab so its viable to be get from pool again
        /// </summary>
        /// <param name="obj"> prefab to return to pool</param>
        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);
        }
    }
}
