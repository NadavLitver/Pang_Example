using Cysharp.Threading.Tasks;
using model;
using System;
using UnityEngine;
using UnityEngine.UI;
using view;
using Zenject;

namespace controller
{
    public class UpgradeHandler : IUpgradeHandler// The Upgrade handler is responsible for all logic correlated with the upgrade panel
    {
        //controllers

        private readonly ILocomotion robotController;
        private readonly ISoundManager soundManager;
        private readonly IPlayerHPHandler playerHPHandler;
        //data and refrences to components
        private readonly GameObject upgradePanel;
        private readonly Button speedButton;
        private readonly Button healthButton;
        private readonly Button laserUpgradeButton;
        private readonly UpgradesConfig upgradesData;
        private bool didChooseUpgrade;
        //events
        public Action<int> OnHPUpgraded { get; set; }
        public Action OnLasersUpgraded { get; set; }


        [Inject]
        public UpgradeHandler(
             GameObject upgradePanel,
             [Inject(Id = "SpeedButton")] Button _speedButton,
             [Inject(Id = "HealthButton")] Button _healthButton,
             [Inject(Id = "LaserUpgradeButton")] Button _laserUpgradeButton,
             UpgradesConfig _upgradesData,
             ILocomotion _robotController,
             IPlayerHPHandler _playerHPHandler,
             ISoundManager _soundManager)
        {//Init refrences after injection
            this.upgradePanel = upgradePanel;
            this.speedButton = _speedButton;
            this.healthButton = _healthButton;
            this.laserUpgradeButton = _laserUpgradeButton;
            this.upgradesData = _upgradesData;
            this.robotController = _robotController;
            this.playerHPHandler = _playerHPHandler;
            this.soundManager = _soundManager;

            Initialize();
        }
        private void Initialize()//subscribe to events
        {
          
            //subscrive to events. each upgrade option to its corresponding upgrade
            laserUpgradeButton.onClick.AddListener(UnSubscribeFromReturningLasers);
            speedButton.onClick.AddListener(UpgradeSpeed);
            healthButton.onClick.AddListener(UpgradeHP);
            OnHPUpgraded += playerHPHandler.AddHp;
        }

        private void UnSubscribeFromReturningLasers()
        {

            OnLasersUpgraded.Invoke();
            didChooseUpgrade = true;
        }
        private void UpgradeSpeed()
        {
            // set a new speed
            robotController.SetSpeed(upgradesData.NewSpeed);
            didChooseUpgrade = true;

        }
        private void UpgradeHP()
        {
            // add health points to existing
            OnHPUpgraded.Invoke(upgradesData.AdditionalHP);
            didChooseUpgrade = true;

        }
        private void TurnOffOnUpgradePanel(bool isOn)
        {
            upgradePanel.SetActive(isOn);
        }
        public async UniTask UpgradeRoutine()
        {
            TurnOffOnUpgradePanel(true);
            await UniTask.WaitUntil(() => didChooseUpgrade);
            soundManager.Play(SoundManager.Sound.UpgradeSelected);
            TurnOffOnUpgradePanel(false);
        }
    }
}
