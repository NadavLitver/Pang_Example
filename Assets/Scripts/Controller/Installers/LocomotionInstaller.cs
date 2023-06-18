using UnityEngine;
using Zenject;
namespace controller
{
    
    public class LocomotionInstaller : MonoInstaller
    {
        [SerializeField] private Transform  locomotionTransform;
        /// <summary>
        /// bind the robots/player transform
        /// create and install the players locomotion
        /// </summary>
        public override void InstallBindings()
        {
            Container.Bind<Transform>().FromInstance(locomotionTransform).WhenInjectedInto<LocoMotion>();
            Container.BindInterfacesTo<LocoMotion>().FromNew().AsSingle().NonLazy();

        }
    }
}