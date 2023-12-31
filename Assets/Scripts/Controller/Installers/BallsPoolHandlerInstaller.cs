using model;
using UnityEngine;
using Zenject;
namespace controller
{
    [CreateAssetMenu(fileName = "BallsPoolHandlerInstaller", menuName = "Installers/BallsPoolHandlerInstaller", order = 20)]

    public class BallsPoolHandlerInstaller : ScriptableObjectInstaller<BallsPoolHandlerInstaller>
    {
        /// <summary>
        /// create and install the ball Pool handler (handels data thats why "Using model"
        /// </summary>
        public override void InstallBindings()
        {
          

            Container.Bind<IBallsPoolHandler>().To<BallsPoolHandler>().FromNew().AsSingle();
        }
    }
}