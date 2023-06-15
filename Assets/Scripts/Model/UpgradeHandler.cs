using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace model
{
    public class UpgradeHandler : MonoBehaviour// The Upgrade handler is responsible for all logic correlated with the upgrade panel
    {
        [SerializeField] GameObject UpgradePanel;
        [SerializeField] Button SpeedButton;
        [SerializeField] Button HealthButton;
        [SerializeField] Button LaserUpgradeButton;
        [SerializeField] ShootController shootController;
        [SerializeField] GameManager gameManagerRef;
        [SerializeField] RobotController robotControllerRef;
        [SerializeField] float newSpeed = 5;
        [SerializeField] int healthPointsToAdd = 2;
        private bool didChooseUpgrade;

        private void Start()
        {
            LaserUpgradeButton.onClick.AddListener(UnSubscribeFromReturningLasers);
            SpeedButton.onClick.AddListener(UpgradeSpeed);
            HealthButton.onClick.AddListener(UpgradeHP);
        }
        public void UnSubscribeFromReturningLasers()
        {
            foreach (var laser in shootController.laserPool.Pool)
            {
                // get laser handler
                LaserHandler currentLaserHandler = laser.GetComponent<LaserHandler>();
                // remove listener so laser does not destroy upon hitting ball
                currentLaserHandler.onHitBall.RemoveListener(shootController.ReturnLaser);
            }
            didChooseUpgrade = true;
        }
        public void UpgradeSpeed()
        {
            // set a new speed
            robotControllerRef.SetSpeed(newSpeed);
            didChooseUpgrade = true;

        }
        public void UpgradeHP()
        {
            // add health points to existing
            gameManagerRef.AddHealthPoints(healthPointsToAdd);
            didChooseUpgrade = true;

        }
        public void TurnOffOnUpgradePanel(bool isOn)
        {
            UpgradePanel.SetActive(isOn);
        }
        public IEnumerator UpgradeRoutine()
        {
            TurnOffOnUpgradePanel(true);
            yield return new WaitUntil(() => didChooseUpgrade);// wait for player to make choice
            // call sound for upgrade
            SoundManager.Play(SoundManager.Sound.UpgradeSelected);
            TurnOffOnUpgradePanel(false);
        }
    }
}
