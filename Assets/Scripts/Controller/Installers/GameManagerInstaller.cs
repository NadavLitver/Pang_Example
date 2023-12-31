using UnityEngine;
using Zenject;
namespace controller
{
    [CreateAssetMenu(fileName = "GameManagerInstaller", menuName = "Installers/GameManagerInstaller",order = 1)]
    public class GameManagerInstaller : ScriptableObjectInstaller<GameManagerInstaller>
    {
        /// <summary>
        ///  create and install the GameManager
        /// </summary>
        public override void InstallBindings()
        {
            Container.Bind<IGameManager>().To<GameManager>().FromNew().AsSingle().NonLazy();

        }
    }
}