using UnityEngine;
using Zenject;
namespace controller
{
    [CreateAssetMenu(fileName = "ShootControllerInstaller", menuName = "Installers/ShootControllerInstaller", order = 7)]
    public class ShootControllerInstaller : ScriptableObjectInstaller<ShootControllerInstaller>
    {
        /// <summary>
        /// create and install the shoot controller
        /// </summary>
        public override void InstallBindings()
        {
            Container.Bind<IShootController>().To<ShootController>().FromNew().AsSingle().NonLazy();

        }
    }
}