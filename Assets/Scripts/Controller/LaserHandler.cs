using model;
using System;
using UnityEngine;
using Zenject;

namespace controller
{
    public class LaserHandler : MonoBehaviour, ILaserHandler// the laser handler handels individual projectile logic
    {
        //data
        public LaserConfig LaserData;
        private float currentTimeAlive;
        public GameObject myGameObject => gameObject;
        //controller
        [Inject] IBallController ballController;
        [Inject] IGameManager gameManager;
        [Inject] IUpgradeHandler upgradeHandler;
        //events
        public Action<ILaserHandler, IBall> OnHitBall { get; set; }
        private void Awake()
        {

            //add listeners
            OnHitBall += ballController.SplitBall;
            OnHitBall += ReturnSelfToPool;
            OnHitBall += gameManager.UpdateScoreOnSplitBall;
            // subscribe to upgrade handlers laser upgrade
            upgradeHandler.OnLasersUpgraded += UnSubscribeFromReturningToPool;
        }
        private void UnSubscribeFromReturningToPool()
        {
            OnHitBall -= (ReturnSelfToPool);
        }
        public void ReturnSelfToPool()//returning too object pool by set game object off
        {
            this.gameObject.SetActive(false);

        }
        public void ReturnSelfToPool(ILaserHandler laser, IBall ball)//returning too object pool by set game object off/ overload
        {
            this.gameObject.SetActive(false);

        }

        private void OnEnable()//reset time alive when enabled
        {
            currentTimeAlive = 0;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Ball"))//identify collision with ball and invoke an event
            {
                OnHitBall?.Invoke(this, collision.gameObject.GetComponent<IBall>());
            }
        }
        private void Update()
        {
            transform.Translate(LaserData.Speed * Time.deltaTime * Vector2.up);// move laser in strait line upwards

            CheckTimeToLive();

        }

        public void CheckTimeToLive()// after some time return laser back to pool
        {
            currentTimeAlive += Time.deltaTime;
            if (currentTimeAlive > LaserData.TimeToLive)
            {
                ReturnSelfToPool();
            }
        }


    }
}
