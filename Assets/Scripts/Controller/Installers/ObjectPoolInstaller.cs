using model;
using UnityEngine;
using Zenject;

namespace controller
{
    [CreateAssetMenu(fileName = "ObjectPoolInstaller", menuName = "Installers/ObjectPoolInstaller", order = 29)]
    public class ObjectPoolInstaller : ScriptableObjectInstaller<ObjectPoolInstaller>
    {
        public Ball prefabRefA;
        public LaserHandler prefabRefB;
        /// <summary>
        /// create and install the object pools and pass arguments for their corresponding consructors (ObjectPool base class)
        /// </summary>
        public override void InstallBindings()
        {
          

            Container.Bind<IObjectPool<Ball>>().To<ObjectPool<Ball>>().AsSingle().WithArguments(prefabRefA,Container);
            Container.Bind<IObjectPool<LaserHandler>>().To<ObjectPool<LaserHandler>>().AsSingle().WithArguments(prefabRefB, Container);
        }
    }
}
