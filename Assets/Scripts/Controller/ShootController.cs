using UnityEngine;
using UnityEngine.Events;
using view;
using model;

namespace controller
{
    public class ShootController : MonoBehaviour// shootController handles all logic that involves shooting the lasers
    {
        //controller elements
        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private BallController ballController;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private RobotAnimatorUpdater robotAnimatorUpdater;
        [SerializeField] private Transform shootPoint;

        //view elements
        [SerializeField] private SoundManager soundManager;

        //data elements
        [SerializeField] private ObjectPool laserPool;
        [SerializeField] private PlayerConfig playerData;
       
      
        
        private float lastTimeShot;
        public UnityEvent onShot;
        private void Start()
        {
            inputHandler.onShoot.AddListener(Shoot);
            onShot.AddListener(robotAnimatorUpdater.PlayShooting);
            foreach (var laser in laserPool.Pool)
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
                GameObject current = laserPool.GetFromPool();
                current.transform.position = shootPoint.position;
                onShot?.Invoke();
                lastTimeShot = Time.time;
                soundManager.Play(SoundManager.Sound.playerShoot);
            }
            
        }

        private bool CheckShootCooldown()//Check if shot is on cooldown
        {
            return Time.time - lastTimeShot > playerData.ShootCD;
        }

        public void ReturnLaser(LaserHandler laserHandler, Rigidbody2D ballHit)
        {
            laserPool.ReturnToPool(laserHandler.gameObject);
        }

       
    }
}