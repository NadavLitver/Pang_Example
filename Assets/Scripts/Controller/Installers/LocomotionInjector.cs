using UnityEngine;
using Zenject;
namespace controller
{
    [CreateAssetMenu(fileName = "LocomotionInjector", menuName = "Installers/LocomotionInjector", order = 5)]

    public class LocomotionInjector : ScriptableObjectInstaller<LocomotionInjector>
    {

        public override void InstallBindings()
        {
            Container.Bind<ILocomotion>().To<LocoMotion>().FromComponentInHierarchy().AsSingle();

        }
    }
}