using UnityEngine;
using Zenject;
namespace controller
{
    [CreateAssetMenu(fileName = "ShootControllerInstaller", menuName = "Installers/ShootControllerInstaller", order = 7)]
    public class ShootControllerInstaller : ScriptableObjectInstaller<ShootControllerInstaller>
    {
        public override void InstallBindings()
        {
            Debug.Log("Trying to bind ShootController");
            Container.Bind<IShootController>().To<ShootController>().FromNew().AsSingle();

        }
    }
}