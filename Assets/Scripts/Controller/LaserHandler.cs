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
        public LaserConfig LaserData;
        public GameObject myGameObject => gameObject;
        [Inject] IBallController ballController;
        [Inject] IGameManager gameManager;

        private float currentTimeAlive;
        private void Start()
        {
            OnHitBall = new UnityEvent<ILaserHandler, Rigidbody2D>();
            OnHitBall.AddListener(ballController.SplitBall);
            OnHitBall.AddListener(gameManager.UpdateScoreOnSplitBall);
            OnHitBall.AddListener(ReturnSelfToPool);
        }

        public void ReturnSelfToPool(ILaserHandler _laser, Rigidbody2D _ballRB2D)
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
            }
        }
        private void Update()
        {
            transform.Translate(LaserData.Speed * Vector2.up * Time.deltaTime);

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
