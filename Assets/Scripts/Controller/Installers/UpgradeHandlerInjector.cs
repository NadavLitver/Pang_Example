using UnityEngine;
using Zenject;
namespace controller
{
    [CreateAssetMenu(fileName = "UpgradeHandlerInjector", menuName = "Installers/UpgradeHandlerInjector", order = 11)]
    public class UpgradeHandlerInjector : ScriptableObjectInstaller<UpgradeHandlerInjector>
    {
        public override void InstallBindings()
        {
            Container.Bind<IUpgradeHandler>().To<UpgradeHandler>().FromComponentInHierarchy().AsSingle();

        }
    }
}