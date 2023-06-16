using model;
using UnityEngine;
using Zenject;
namespace controller
{
    [CreateAssetMenu(fileName = "BallsPoolHandlerInjector", menuName = "Installers/BallsPoolHandlerInjector", order = 20)]

    public class BallsPoolHandlerInjector : ScriptableObjectInstaller<BallsPoolHandlerInjector>
    {
        public override void InstallBindings()
        {
            Container.Bind<IBallsPoolHandler>().To<BallsPoolHandler>().AsSingle();
        }
    }
}