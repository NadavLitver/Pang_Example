using model;
using UnityEngine;
using Zenject;
namespace controller
{
    [CreateAssetMenu(fileName = "BallConfigInstaller", menuName = "Installers/BallConfigInstaller",order = 39)]
    public class BallConfigInstaller : ScriptableObjectInstaller<BallConfigInstaller>
    {
        [SerializeField] BallsConfig ballsConfig;
        /// <summary>
        /// Install PlayerConfig SO from instance
        /// </summary>
        public override void InstallBindings()
        {
            Container.Bind<BallsConfig>().FromInstance(ballsConfig);
        }
    }
}