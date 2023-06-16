using UnityEngine;
using Zenject;
namespace controller
{
    [CreateAssetMenu(fileName = "ShootControllerInjector", menuName = "Installers/ShootControllerInjector", order = 7)]
    public class ShootControllerInjector : ScriptableObjectInstaller<ShootControllerInjector>
    {
        public override void InstallBindings()
        {
            Container.Bind<IShootController>().To<ShootController>().FromComponentInHierarchy().AsSingle();

        }
    }
}