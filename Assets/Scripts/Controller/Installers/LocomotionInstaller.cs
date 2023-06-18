using UnityEngine;
using Zenject;
namespace controller
{
    
    public class LocomotionInstaller : MonoInstaller
    {
        [SerializeField] private LocoMotion locomotionPrefab;
        [SerializeField] private Transform  locomotionTransform;
        public override void InstallBindings()
        {
            // bind the robots/player transform
            Container.Bind<Transform>().FromInstance(locomotionTransform).WhenInjectedInto<LocoMotion>();
            // create(from prefab) and install the players locomotion
            Container.Bind<ILocomotion>().To<LocoMotion>().FromComponentInNewPrefab(locomotionPrefab).AsSingle().NonLazy();

        }
    }
}