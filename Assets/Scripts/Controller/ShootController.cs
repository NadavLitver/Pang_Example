using UnityEngine;
using UnityEngine.Events;
using model;
namespace controller
{
    public class ShootController : MonoBehaviour// shootController handles all logic that involves shooting the lasers
    {
        [SerializeField] InputHandler inputHandler;
        [SerializeField] BallController ballController;
        [SerializeField] GameManager gameManager;
        private float lastTimeShot;
        public UnityEvent onShot;
        [SerializeField] LaserData laserData;
        [SerializeField] PlayerData playerData;

       // [SerializeField] RobotAnimatorUpdater robotAnimatorUpdater;
        private void Start()
        {
            inputHandler.onShoot.AddListener(Shoot);
          //  shootEvent.AddListener(robotAnimatorUpdater.PlayShooting);
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
               // current.transform.position = robotAnimatorUpdater.transform.position + Vector3.up;
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