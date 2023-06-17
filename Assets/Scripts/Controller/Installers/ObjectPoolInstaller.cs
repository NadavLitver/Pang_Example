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
        public ObjectPool ballPool;
        public ObjectPool laserPool;
        public override void InstallBindings()
        {

        
            Container.BindInstance(prefabRefA).WhenInjectedIntoInstance(ballPool);
            Container.BindInstance(prefabRefB).WhenInjectedIntoInstance(laserPool);

            // Bind the object pools with the specified IDs to get correct instance when binding to interface
            Container.Bind<IObjectPool>().WithId("BallPool").To<ObjectPool>().FromInstance(ballPool);
            Container.Bind<IObjectPool>().WithId("LaserPool").To<ObjectPool>().FromInstance(laserPool);

        
        }
    }
}
/*
using model;
using UnityEngine;
using Zenject;
namespace controller
{
    public class ObjectPoolInstaller : MonoInstaller
    {
        public GameObject prefabRefA;
        public GameObject prefabRefB;
        public ObjectPool BallPool;
        public ObjectPool LaserPool;


        public override void InstallBindings()
        {

            // Bind the prefab references to the respective object pools

            Container.BindInstance(prefabRefA).WhenInjectedIntoInstance(BallPool);
            Container.BindInstance(prefabRefB).WhenInjectedIntoInstance(LaserPool);

            // Bind the object pools with the specified IDs to get correct instance when binding to interface
            Container.Bind<IObjectPool>().WithId("BallPool").To<ObjectPool>().FromInstance(BallPool);
            Container.Bind<IObjectPool>().WithId("LaserPool").To<ObjectPool>().FromInstance(LaserPool);

        }

    }
}
*/