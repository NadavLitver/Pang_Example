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
            Container.Bind<Transform>().FromInstance(locomotionTransform).WhenInjectedInto<LocoMotion>();

            Container.Bind<ILocomotion>().To<LocoMotion>().FromComponentInNewPrefab(locomotionPrefab).AsSingle().NonLazy();

        }
    }
}