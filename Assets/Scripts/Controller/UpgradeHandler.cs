using model;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using view;

namespace controller
{
    public class UpgradeHandler : MonoBehaviour// The Upgrade handler is responsible for all logic correlated with the upgrade panel
    {
        //UI elements
        [SerializeField] private GameObject upgradePanel;
        [SerializeField] private Button speedButton;
        [SerializeField] private Button healthButton;
        [SerializeField] private Button laserUpgradeButton;
        // Data elements
        [SerializeField] private ObjectPool laserPool;
        [SerializeField] private UpgradesConfig upgradesData;
        // controllers ellements
        [SerializeField] private ShootController shootController;
        [SerializeField] private GameManager gameManagerRef;
        [SerializeField] private LocoMotion robotControllerRef;
        [SerializeField] private SoundManager soundManager;
        private bool didChooseUpgrade;

        private void Start()
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
                currentLaserHandler.onHitBall.RemoveListener(shootController.ReturnLaser);
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
            gameManagerRef.AddHealthPointsAndUpdateUI(upgradesData.AdditionalHP);
            didChooseUpgrade = true;

        }
        private void TurnOffOnUpgradePanel(bool isOn)
        {
            upgradePanel.SetActive(isOn);
        }
        public IEnumerator UpgradeRoutine()
        {
            TurnOffOnUpgradePanel(true);
            yield return new WaitUntil(() => didChooseUpgrade);// wait for player to make choice
            // call sound for upgrade
            soundManager.Play(SoundManager.Sound.UpgradeSelected);
            TurnOffOnUpgradePanel(false);
        }
    }
}
