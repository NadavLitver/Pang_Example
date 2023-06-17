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
        private readonly IRobotAnimatorUpdater robotAnimatorUpdater;

        //view elements
        private readonly ISoundManager soundManager;

        //data elements

        private readonly PlayerConfig playerData;

        private readonly IObjectPool laserPool;// not


        private float lastTimeShot;
        public UnityEvent OnShot { get; private set; }

        [Inject]
        public ShootController(IInputHandler _inputHandler, IRobotAnimatorUpdater _robotAnimatorUpdater, ISoundManager _soundManager, PlayerConfig _playerData, [Inject(Id = "LaserPool")] IObjectPool _laserPool)
        {
            Debug.Log("Shoot Controller Constructor Called");
            // Init Refrences
            this.inputHandler = _inputHandler;
            this.robotAnimatorUpdater = _robotAnimatorUpdater;
            this.soundManager = _soundManager;
            this.playerData = _playerData;
            this.laserPool = _laserPool;


            inputHandler.onShoot = new UnityEvent();
            inputHandler.onShoot.AddListener(Shoot);

            OnShot = new UnityEvent();
            OnShot.AddListener(robotAnimatorUpdater.PlayShooting);


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




    }
}