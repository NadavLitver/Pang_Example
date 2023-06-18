using model;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
namespace controller
{
    public class UpgradeHandlerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject upgradePanel;
        [SerializeField] private Button speedButton;
        [SerializeField] private Button healthButton;
        [SerializeField] private Button laserUpgradeButton;
        [SerializeField] private UpgradesConfig upgradeConfig;
        public override void InstallBindings()
        {
            Container.Bind<GameObject>().FromInstance(upgradePanel).WhenInjectedInto<UpgradeHandler>();
            Container.Bind<Button>().WithId("SpeedButton").FromInstance(speedButton).WhenInjectedInto<UpgradeHandler>();
            Container.Bind<Button>().WithId("HealthButton").FromInstance(healthButton).WhenInjectedInto<UpgradeHandler>();
            Container.Bind<Button>().WithId("LaserUpgradeButton").FromInstance(laserUpgradeButton).WhenInjectedInto<UpgradeHandler>();
            Container.Bind<UpgradesConfig>().FromInstance(upgradeConfig);

            Container.Bind<IUpgradeHandler>().To<UpgradeHandler>().FromNew().AsSingle();

        }
    }
}