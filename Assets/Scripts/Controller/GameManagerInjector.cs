using UnityEngine;
using Zenject;
namespace controller
{
    [CreateAssetMenu(fileName = "GameManagerInjector", menuName = "Installers/GameManagerInjector",order = 1)]
    public class GameManagerInjector : ScriptableObjectInstaller<GameManagerInjector>
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameManager>().To<GameManager>().FromComponentInHierarchy().AsSingle();

        }
    }
}