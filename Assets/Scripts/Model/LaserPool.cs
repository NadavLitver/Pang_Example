using UnityEngine;
using Zenject;

namespace model
{
    public class LaserPool : ObjectPool
    {
        public LaserPool(GameObject _prefabRef, DiContainer container) : base(_prefabRef, container)
        {
        }
    }
}