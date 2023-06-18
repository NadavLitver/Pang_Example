using UnityEngine;
using Zenject;
namespace controller
{
    [CreateAssetMenu(fileName = "GameManagerInstaller", menuName = "Installers/GameManagerInstaller",order = 1)]
    public class GameManagerInstaller : ScriptableObjectInstaller<GameManagerInstaller>
    {
        public override void InstallBindings()
        {
           // create and install the GameManager

            Container.Bind<IGameManager>().To<GameManager>().FromNew().AsSingle().NonLazy();

        }
    }
}