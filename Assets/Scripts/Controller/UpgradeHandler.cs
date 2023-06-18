using model;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
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
        private readonly UpgradesConfig upgradesData;
        private readonly ILocomotion robotController;
        private readonly ISoundManager soundManager;
        private readonly IPlayerHPHandler playerHPHandler;
        private bool didChooseUpgrade;

        public UnityEvent<int> OnHPUpgraded { get; private set; }
        public UnityEvent OnLasersUpgraded { get; private set; }


        [Inject]
        public UpgradeHandler(
             GameObject upgradePanel,
             [Inject(Id = "SpeedButton")] Button _speedButton,
             [Inject(Id = "HealthButton")] Button _healthButton,
             [Inject(Id = "LaserUpgradeButton")] Button _laserUpgradeButton,
             UpgradesConfig _upgradesData,
             ILocomotion _robotController,
             IPlayerHPHandler _playerHPHandler,
             ISoundManager _soundManager)//I dislike wrapping like this but there were alot of arguments
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
            OnHPUpgraded = new UnityEvent<int>();
            OnLasersUpgraded = new UnityEvent();
            laserUpgradeButton.onClick.AddListener(UnSubscribeFromReturningLasers);
            speedButton.onClick.AddListener(UpgradeSpeed);
            healthButton.onClick.AddListener(UpgradeHP);
            OnHPUpgraded.AddListener(playerHPHandler.AddHp);
        }

        private void UnSubscribeFromReturningLasers()
        {
            //foreach (var laser in laserPool.Pool)
            //{
            //    // get laser handler
            //    LaserHandler currentLaserHandler = laser.GetComponent<LaserHandler>();
            //    // remove listener so laser does not destroy upon hitting ball
            //    currentLaserHandler.OnHitBall.RemoveListener(currentLaserHandler.ReturnSelfToPool);
            //}
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
        public IEnumerator UpgradeRoutine()
        {
            
            TurnOffOnUpgradePanel(true);//turn on the panel
            yield return new WaitUntil(() => didChooseUpgrade);// wait for player to make choice
            soundManager.Play(SoundManager.Sound.UpgradeSelected);//play sounds after player chose
            TurnOffOnUpgradePanel(false);// turn off the panel
        }
    }
}
