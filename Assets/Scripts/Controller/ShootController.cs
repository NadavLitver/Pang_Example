using model;
using UnityEngine;
using UnityEngine.Events;
using view;
using Zenject;

namespace controller
{
    public class ShootController : IShootController// shootController handles all logic that involves shooting the lasers
    {
        //controller elements
        private readonly IInputHandler inputHandler;
        private readonly IBallController ballController;
        private readonly IGameManager gameManager;
        private readonly IRobotAnimatorUpdater robotAnimatorUpdater;

        //view elements
        private readonly ISoundManager soundManager;

        //data elements

        private readonly PlayerConfig playerData;

        private readonly IObjectPool laserPool;// not


        private float lastTimeShot;
        public UnityEvent OnShot { get; private set; }
        [Inject]
        public ShootController(IInputHandler _inputHandler, IBallController _ballController, IGameManager _gameManager, IRobotAnimatorUpdater _robotAnimatorUpdater, ISoundManager _soundManager, PlayerConfig _playerData, [Inject(Id = "LaserPool")] IObjectPool _laserPool)
        {
            // Init Refrences
            this.inputHandler = _inputHandler;
            this.ballController = _ballController;
            this.gameManager = _gameManager;
            this.robotAnimatorUpdater = _robotAnimatorUpdater;
            this.soundManager = _soundManager;
            this.playerData = _playerData;
            this.laserPool = _laserPool;


            inputHandler.Init();
            inputHandler.onShoot.AddListener(Shoot);

            OnShot = new UnityEvent();
            OnShot.AddListener(robotAnimatorUpdater.PlayShooting);

            InitializeLaserPool();

        }
        public void InitializeLaserPool()
        {
           

            laserPool.PopulatePool();
            foreach (var laser in laserPool.Pool)
            {
                LaserHandler currentLaserHandler = laser.GetComponent<LaserHandler>();

                currentLaserHandler.OnHitBall.AddListener(ballController.SplitBall);
                currentLaserHandler.OnHitBall.AddListener(gameManager.UpdateScoreOnSplitBall);
                currentLaserHandler.OnHitBall.AddListener(ReturnLaser);
            }
        }
        private void Shoot()
        {
            if (CheckShootCooldown())
            {
                GameObject current = laserPool.GetFromPool();
                current.transform.position = robotAnimatorUpdater.ShootPoint.position;
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