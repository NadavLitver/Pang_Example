using UnityEngine;
using UnityEngine.Events;
using view;
using model;
using Zenject;

namespace controller
{
    public class ShootController : MonoBehaviour,IShootController// shootController handles all logic that involves shooting the lasers
    {
        //controller elements
        [Inject] private IInputHandler inputHandler;
        [Inject] private IBallController ballController;
        [Inject] private IGameManager gameManager;
        [Inject] private IRobotAnimatorUpdater robotAnimatorUpdater;

        //view elements
        [Inject] private ISoundManager soundManager;

        //data elements
        [Inject(Id = "LaserPool")] private IObjectPool laserPool;
        [SerializeField] private PlayerConfig playerData;
        [SerializeField] private Transform shootPoint;
       
      
        
        private float lastTimeShot;
        public UnityEvent OnShot { get; private set; }
        private void Awake()
        {
            OnShot = new UnityEvent();
        }
        private void Start()
        {
            inputHandler.onShoot.AddListener(Shoot);
            OnShot.AddListener(robotAnimatorUpdater.PlayShooting);
            foreach (var laser in laserPool.Pool)
            {
                LaserHandler currentLaserHandler = laser.GetComponent<LaserHandler>();

                //on laser hit ball call the "Split ball" method
                currentLaserHandler.OnHitBall.AddListener(ballController.SplitBall);

                //on laser hit ball call update score
                currentLaserHandler.OnHitBall.AddListener(gameManager.UpdateScoreOnSplitBall);

                // on laser hit ball return laser to object pool
                currentLaserHandler.OnHitBall.AddListener(ReturnLaser);
            }
        }
      

        private void Shoot()
        {
            if (CheckShootCooldown())
            {
                GameObject current = laserPool.GetFromPool();
                current.transform.position = shootPoint.position;
                OnShot?.Invoke();
                lastTimeShot = Time.time;
                soundManager.Play(SoundManager.Sound.playerShoot);
            }
            
        }

        private bool CheckShootCooldown()//Check if shot is on cooldown
        {
            return Time.time - lastTimeShot > playerData.ShootCD;
        }

        public void ReturnLaser(ILaserHandler laserHandler, Rigidbody2D ballHit)
        {
            laserPool.ReturnToPool(laserHandler.myGameObject);
        }

       
    }
}