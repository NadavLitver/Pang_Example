using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace model
{

    public class ObjectPool : MonoBehaviour,IObjectPool
    {
        [Inject] private GameObject prefabRef;
        [SerializeField] private int poolSize = 20;
        private List<GameObject> pool = new List<GameObject>();

        public List<GameObject> Pool => pool;

       
        public void PopulatePool()
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject current = Instantiate(prefabRef);
                current.name = prefabRef.name + i;
                pool.Add(current);
                current.SetActive(false);
            }
        }

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

            GameObject current = Instantiate(prefabRef);
            current.name = prefabRef.name + "Extra";
            pool.Add(current);
            current.SetActive(true);
            return current;
        }

        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);
        }
    }
}
