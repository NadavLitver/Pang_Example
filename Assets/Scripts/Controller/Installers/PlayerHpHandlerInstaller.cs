
using UnityEngine;
using Zenject;
namespace controller
{
    [CreateAssetMenu(fileName = "PlayerHpHandlerInstaller", menuName = "Installers/PlayerHpHandlerInstaller", order = 9)]

    public class PlayerHpHandlerInstaller : ScriptableObjectInstaller<PlayerHpHandlerInstaller>
    {
        /// <summary>
        /// Create and install the PlayerHPHandler
        /// </summary>
        public override void InstallBindings()
        {
            Container.Bind<IPlayerHPHandler>().To<PlayerHPHandler>().FromNew().AsSingle();

        }
    }
}