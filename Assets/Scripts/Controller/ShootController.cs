using model;
using System;
using UnityEngine;
using UnityEngine.Events;
using view;
using Zenject;

namespace controller
{
    public class ShootController : IShootController// shootController handles all logic that involves shooting the lasers
    {
        //controller elements
        private readonly IRobotAnimatorUpdater robotAnimatorUpdater;

        //view elements
        private readonly ISoundManager soundManager;
        private readonly IInputHandler inputHandler;

        //data elements
        private readonly IPlayerConfig playerData;
        private readonly IObjectPool<LaserHandler> laserPool;
        private float lastTimeShot;
        //events
        public Action OnShot { get; set; }

        [Inject]
        public ShootController(IInputHandler _inputHandler, IRobotAnimatorUpdater _robotAnimatorUpdater, ISoundManager _soundManager, IPlayerConfig _playerData, IObjectPool<LaserHandler> _laserPool)
        {
            // Init Refrences
            this.inputHandler = _inputHandler;
            this.robotAnimatorUpdater = _robotAnimatorUpdater;
            this.soundManager = _soundManager;
            this.playerData = _playerData;
            this.laserPool = _laserPool;

            //subscribe to events
           
            inputHandler.OnTapScreen += Shoot;
            OnShot += robotAnimatorUpdater.PlayShooting;


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