using UnityEngine;
using Zenject;

namespace controller
{
    [CreateAssetMenu(fileName = "BallControllerInstaller", menuName = "Installers/BallControllerInstaller", order = 3)]

    public class BallControllerInstaller : ScriptableObjectInstaller<BallControllerInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IBallController>().To<BallController>().FromComponentInHierarchy().AsSingle();

        }
    }
}