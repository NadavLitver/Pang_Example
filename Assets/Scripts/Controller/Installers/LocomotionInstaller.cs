using UnityEngine;
using Zenject;
namespace controller
{
    
    public class LocomotionInstaller : MonoInstaller
    {
        [SerializeField] private LocoMotion locomotionPrefab;
        [SerializeField] private Transform  locomotionTransform;
        /// <summary>
        /// bind the robots/player transform
        /// create(from prefab) and install the players locomotion
        /// </summary>
        public override void InstallBindings()
        {
            Container.Bind<Transform>().FromInstance(locomotionTransform).WhenInjectedInto<LocoMotion>();
            Container.Bind<ILocomotion>().To<LocoMotion>().FromComponentInNewPrefab(locomotionPrefab).AsSingle().NonLazy();

        }
    }
}