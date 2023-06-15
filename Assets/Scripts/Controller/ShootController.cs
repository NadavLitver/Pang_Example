using UnityEngine;
using UnityEngine.Events;

namespace controller
{
    public class ShootController : MonoBehaviour// shootController handles all logic that involves shooting the lasers
    {
        //controller elements
        [SerializeField] InputHandler inputHandler;
        [SerializeField] BallController ballController;
        [SerializeField] GameManager gameManager;
        //data elements
        [SerializeField] model.LaserData laserData;
        [SerializeField] model.PlayerData playerData;
        // view elements
        [SerializeField] view.RobotAnimatorUpdater robotAnimatorUpdater;
        //
        private float lastTimeShot;
        public UnityEvent onShot;
        private void Start()
        {
            inputHandler.onShoot.AddListener(Shoot);
            onShot.AddListener(robotAnimatorUpdater.PlayShooting);
            foreach (var laser in laserData.LaserPool.Pool)
            {
                LaserHandler currentLaserHandler = laser.GetComponent<LaserHandler>();

                //on laser hit ball call the "Split ball" method
                currentLaserHandler.onHitBall.AddListener(ballController.SplitBall);

                //on laser hit ball call update score
                currentLaserHandler.onHitBall.AddListener(gameManager.UpdateScoreOnSplitBall);

                // on laser hit ball return laser to object pool
                currentLaserHandler.onHitBall.AddListener(ReturnLaser);
            }
        }
      

        private void Shoot()
        {
            if (CheckShootCooldown())
            {
                GameObject current = laserData.LaserPool.GetFromPool();
                current.transform.position = robotAnimatorUpdater.transform.position + Vector3.up;
                onShot?.Invoke();
                lastTimeShot = Time.time;
                SoundManager.Play(SoundManager.Sound.playerShoot);
            }
            
        }

        private bool CheckShootCooldown()//Check if shot is on cooldown
        {
            return Time.time - lastTimeShot > playerData.ShootCD;
        }

        public void ReturnLaser(LaserHandler laserHandler, Rigidbody2D ballHit)
        {
            laserData.LaserPool.ReturnToPool(laserHandler.gameObject);
        }

       
    }
}