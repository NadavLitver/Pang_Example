using UnityEngine;
using Zenject;

namespace model
{
    public class BallPool : ObjectPool//this class exists so I can use "As Single" in the installer
    {
        public BallPool(GameObject _prefabRef, DiContainer container) : base(_prefabRef, container)
        {
        }
    }
}