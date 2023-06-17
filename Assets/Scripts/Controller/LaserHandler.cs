using UnityEngine;
using UnityEngine.Events;
using model;
using Zenject;
using System;

namespace controller
{
    public class LaserHandler : MonoBehaviour, ILaserHandler// the laser handler handels individual projectile logic
    {
        public UnityEvent<ILaserHandler, Rigidbody2D> OnHitBall { get; private set; }
        public UnityEvent OnHitBallNoArgs;

        public LaserConfig LaserData;
        public GameObject myGameObject => gameObject;
        [Inject] IBallController ballController;
        [Inject] IGameManager gameManager;
        [Inject] IUpgradeHandler upgradeHandler;

        private float currentTimeAlive;
        private void Awake()
        {
            OnHitBall = new UnityEvent<ILaserHandler, Rigidbody2D>();
            OnHitBall.AddListener(ballController.SplitBall);
            OnHitBallNoArgs.AddListener(ReturnSelfToPool);
            OnHitBallNoArgs.AddListener(gameManager.UpdateScoreOnSplitBall);
            upgradeHandler.OnLasersUpgraded.AddListener(UnSubscribeFromReturningToPool);
        }
        private void UnSubscribeFromReturningToPool()
        {
            OnHitBallNoArgs.RemoveListener(ReturnSelfToPool);
        }

        public void ReturnSelfToPool()
        {
            this.gameObject.SetActive(false);

        }

        private void OnEnable()
        {
            currentTimeAlive = 0;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Ball"))
            {
                OnHitBall?.Invoke(this, collision.gameObject.GetComponent<Rigidbody2D>());
                OnHitBallNoArgs.Invoke();
            }
        }
        private void Update()
        {
            transform.Translate(LaserData.Speed * Time.deltaTime * Vector2.up);

            CheckTimeToLive();

        }

        public void CheckTimeToLive()
        {
            currentTimeAlive += Time.deltaTime;
            if (currentTimeAlive > LaserData.TimeToLive)
            {
                this.gameObject.SetActive(false);
            }
        }
      
       
    }
}
