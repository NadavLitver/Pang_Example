
using UnityEngine;
using Zenject;
namespace controller
{
    [CreateAssetMenu(fileName = "PlayerHpHandlerInjector", menuName = "Installers/PlayerHpHandlerInjector", order = 9)]

    public class PlayerHpHandlerInjector : ScriptableObjectInstaller<PlayerHpHandlerInjector>
    {
        public override void InstallBindings()
        {
            Container.Bind<IPlayerHPHandler>().To<PlayerHPHandler>().FromComponentInHierarchy().AsSingle();

        }
    }
}