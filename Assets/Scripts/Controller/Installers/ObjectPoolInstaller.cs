using model;
using UnityEngine;
using Zenject;

namespace controller
{
    [CreateAssetMenu(fileName = "ObjectPoolInstaller", menuName = "Installers/ObjectPoolInstaller", order = 29)]
    public class ObjectPoolInstaller : ScriptableObjectInstaller<ObjectPoolInstaller>
    {
        public GameObject prefabRefA;
        public GameObject prefabRefB;
        /// <summary>
        /// create and install the object pools and pass arguments for their corresponding consructors (ObjectPool base class)
        /// </summary>
        public override void InstallBindings()
        {


         

          
            Container.Bind<IObjectPool>().WithId("BallPool").To<BallPool>().AsSingle().WithArguments(prefabRefA,Container);
            Container.Bind<IObjectPool>().WithId("LaserPool").To<LaserPool>().AsSingle().WithArguments(prefabRefB, Container);

        
        }
    }
}
