using model;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using view;
using Zenject;

namespace controller
{
    public class UpgradeHandler : IUpgradeHandler// The Upgrade handler is responsible for all logic correlated with the upgrade panel
    {
        private readonly GameObject upgradePanel;
        private readonly Button speedButton;
        private readonly Button healthButton;
        private readonly Button laserUpgradeButton;
        private readonly IObjectPool laserPool;
        private readonly UpgradesConfig upgradesData;
        private readonly IShootController shootController;
        private readonly IGameManager gameManager;
        private readonly ILocomotion robotControllerRef;
        private readonly ISoundManager soundManager;
        private bool didChooseUpgrade;

        [Inject]
        public UpgradeHandler(
             GameObject upgradePanel,
             [Inject(Id = "SpeedButton")] Button speedButton,
             [Inject(Id = "HealthButton")] Button healthButton,
             [Inject(Id = "LaserUpgradeButton")] Button laserUpgradeButton,
             [Inject(Id = "LaserPool")] IObjectPool laserPool,
             UpgradesConfig upgradesData,
             IShootController shootController,
             IGameManager gameManager,
             ILocomotion robotControllerRef,
             ISoundManager soundManager)//I dislike wrapping like this but there were alot of arguments
        {//Init refrences after injection
            this.upgradePanel = upgradePanel;
            this.speedButton = speedButton;
            this.healthButton = healthButton;
            this.laserUpgradeButton = laserUpgradeButton;
            this.laserPool = laserPool;
            this.upgradesData = upgradesData;
            this.shootController = shootController;
            this.gameManager = gameManager;
            this.robotControllerRef = robotControllerRef;
            this.soundManager = soundManager;

            Initialize();
        }
        private void Initialize()//subscribe to events
        {
            laserUpgradeButton.onClick.AddListener(UnSubscribeFromReturningLasers);
            speedButton.onClick.AddListener(UpgradeSpeed);
            healthButton.onClick.AddListener(UpgradeHP);
        }

        private void UnSubscribeFromReturningLasers()
        {
            foreach (var laser in laserPool.Pool)
            {
                // get laser handler
                LaserHandler currentLaserHandler = laser.GetComponent<LaserHandler>();
                // remove listener so laser does not destroy upon hitting ball
                currentLaserHandler.OnHitBall.RemoveListener(shootController.ReturnLaser);
            }
            didChooseUpgrade = true;
        }
        private void UpgradeSpeed()
        {
            // set a new speed
            robotControllerRef.SetSpeed(upgradesData.NewSpeed);
            didChooseUpgrade = true;

        }
        private void UpgradeHP()
        {
            // add health points to existing
            gameManager.AddHealthPointsAndUpdateUI(upgradesData.AdditionalHP);
            didChooseUpgrade = true;

        }
        private void TurnOffOnUpgradePanel(bool isOn)
        {
            upgradePanel.SetActive(isOn);
        }
        public IEnumerator UpgradeRoutine()
        {
            
            TurnOffOnUpgradePanel(true);//turn on the panel
            yield return new WaitUntil(() => didChooseUpgrade);// wait for player to make choice
            soundManager.Play(SoundManager.Sound.UpgradeSelected);//play sounds after player chose
            TurnOffOnUpgradePanel(false);// turn off the panel
        }
    }
}
