using UnityEngine;
using Zenject;
namespace controller
{
    [CreateAssetMenu(fileName = "ShootControllerInstaller", menuName = "Installers/ShootControllerInstaller", order = 7)]
    public class ShootControllerInstaller : ScriptableObjectInstaller<ShootControllerInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IShootController>().To<ShootController>().FromNew().AsSingle().NonLazy();// this one is non lazy because it doesnt have refrences but needs to be created on the beggining

        }
    }
}