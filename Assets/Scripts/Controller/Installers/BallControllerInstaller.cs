using UnityEngine;
using Zenject;

namespace controller
{
    [CreateAssetMenu(fileName = "BallControllerInstaller", menuName = "Installers/BallControllerInstaller", order = 3)]

    public class BallControllerInstaller : ScriptableObjectInstaller<BallControllerInstaller>
    {
        [SerializeField] private BallController ballControllerPrefab;
        /// <summary>
        /// create(from prefab) and install the ball controller
        /// </summary>
        public override void InstallBindings()
        {
           
            Container.Bind<IBallController>().To<BallController>().FromComponentInNewPrefab(ballControllerPrefab).AsSingle();

        }
    }
}