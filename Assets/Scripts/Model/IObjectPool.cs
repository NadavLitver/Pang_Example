using System.Collections.Generic;
using UnityEngine;
namespace model
{
    public interface IObjectPool
    {
        List<GameObject> Pool { get; }
        GameObject GetFromPool();
        void ReturnToPool(GameObject obj);
        void PopulatePool();
    }
}