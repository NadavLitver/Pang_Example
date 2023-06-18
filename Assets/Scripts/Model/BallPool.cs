using UnityEngine;
using Zenject;

namespace model
{
    public class BallPool : ObjectPool
    {
        public BallPool(GameObject _prefabRef, DiContainer container) : base(_prefabRef, container)
        {
        }
    }
}