using System.Collections.Generic;
using UnityEngine;
namespace model
{

    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] GameObject PrefabRef;
        [SerializeField] int PoolSize = 20;
        List<GameObject> pool;

        public List<GameObject> Pool { get => pool;}

        private void Awake()
        {
            pool = new List<GameObject>();
            PopulatePool();
        }
        private void PopulatePool()
        {
            //Populate the GameObject pool list by the GameObject pool size 
            for (int i = 0; i < PoolSize; i++)
            {
                GameObject current = Instantiate(PrefabRef);
                current.name = PrefabRef.name + i;
                pool.Add(current);
                current.SetActive(false);
            }
        }
        public GameObject GetFromPool()
        {
            // Find an inactive GameObject in the pool and return it
            for (int i = 0; i < pool.Count; i++)
            {
                if (!pool[i].activeInHierarchy)
                {
                    pool[i].SetActive(true);
                    return pool[i];
                }
            }

            // If no inactive GameObject is available, create a new one and return it
            GameObject current = Instantiate(PrefabRef);
            current.name = PrefabRef.name + "Extra";
            pool.Add(current);
            current.SetActive(true);
            return current;
        }
        public void ReturnToPool(GameObject ball)
        {
            // Deactivate the GameObject and return it to the pool
            ball.SetActive(false);
        }
    }
}
