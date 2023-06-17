using UnityEngine;
using Zenject;
namespace controller
{
    [CreateAssetMenu(fileName = "LocomotionInstaller", menuName = "Installers/LocomotionInstaller", order = 5)]

    public class LocomotionInstaller : ScriptableObjectInstaller<LocomotionInstaller>
    {

        public override void InstallBindings()
        {
            Container.Bind<ILocomotion>().To<LocoMotion>().FromComponentInHierarchy().AsSingle();

        }
    }
}