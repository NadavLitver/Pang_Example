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
        private readonly IObjectPool<LaserHandler> laserPool;
        private float lastTimeShot;
        //events
        public UnityEvent OnShot { get; private set; }

        [Inject]
        public ShootController(IInputHandler _inputHandler, IRobotAnimatorUpdater _robotAnimatorUpdater, ISoundManager _soundManager, PlayerConfig _playerData, IObjectPool<LaserHandler> _laserPool)
        {
            // Init Refrences
            this.inputHandler = _inputHandler;
            this.robotAnimatorUpdater = _robotAnimatorUpdater;
            this.soundManager = _soundManager;
            this.playerData = _playerData;
            this.laserPool = _laserPool;

            //Init and subscribe to events
            inputHandler.OnTapScreen = new UnityEvent();//the constructor is being called before the input handler ( a mono ) awake is called so i initialized the event here, maybe a good reason to use cs actions instead
            inputHandler.OnTapScreen.AddListener(Shoot);

            OnShot = new UnityEvent();
            OnShot.AddListener(robotAnimatorUpdater.PlayShooting);


        }

        private void Shoot()// after player tapped screen check for cooldown of "Shoot" if the shoot is not on cooldown execute the shot
        {
            if (CheckShootCooldown())
            {
                GameObject current = laserPool.GetFromPool().gameObject;
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