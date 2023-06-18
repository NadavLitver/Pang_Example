using UnityEngine;
using Zenject;

namespace controller
{
    [CreateAssetMenu(fileName = "BallControllerInstaller", menuName = "Installers/BallControllerInstaller", order = 3)]

    public class BallControllerInstaller : ScriptableObjectInstaller<BallControllerInstaller>
    {
        /// <summary>
        /// create and install the ball controller
        /// </summary>
        public override void InstallBindings()
        {

            Container.BindInterfacesTo<BallController>().FromNew().AsSingle();

        }
    }
}