using UnityEngine;
using Zenject;

namespace controller
{
    [CreateAssetMenu(fileName = "BallControllerInstaller", menuName = "Installers/BallControllerInstaller", order = 3)]

    public class BallControllerInstaller : ScriptableObjectInstaller<BallControllerInstaller>
    {
        [SerializeField] private BallController ballControllerPrefab;

        public override void InstallBindings()
        {
            // create(from prefab) and install the ball controller
            Container.Bind<IBallController>().To<BallController>().FromComponentInNewPrefab(ballControllerPrefab).AsSingle();

        }
    }
}