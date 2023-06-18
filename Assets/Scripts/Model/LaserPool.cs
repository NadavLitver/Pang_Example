using UnityEngine;
using Zenject;

namespace model
{
    public class LaserPool : ObjectPool//this class exists so I can use "As Single" in the installer
    {
        public LaserPool(GameObject _prefabRef, DiContainer container) : base(_prefabRef, container)
        {
        }
    }
}