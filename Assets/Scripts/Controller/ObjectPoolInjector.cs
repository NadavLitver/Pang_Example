using UnityEngine;
using Zenject;
using model;
public class ObjectPoolInjector : MonoInstaller
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