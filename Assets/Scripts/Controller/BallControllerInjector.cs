using UnityEngine;
using Zenject;

namespace controller
{
    [CreateAssetMenu(fileName = "BallControllerInjector", menuName = "Installers/BallControllerInjector", order = 3)]

    public class BallControllerInjector : ScriptableObjectInstaller<BallControllerInjector>
    {
        public override void InstallBindings()
        {
            Container.Bind<IBallController>().To<BallController>().FromComponentInHierarchy().AsSingle();

        }
    }
}