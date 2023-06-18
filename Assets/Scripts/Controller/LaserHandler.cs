using model;
using UnityEngine;
using UnityEngine.Events;
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
        public UnityEvent<ILaserHandler, Ball> OnHitBall { get; private set; }
        private void Awake()
        {
            OnHitBall = new UnityEvent<ILaserHandler, Ball>();//init event
            //add listeners
            OnHitBall.AddListener(ballController.SplitBall);
            OnHitBall.AddListener(ReturnSelfToPool);
            OnHitBall.AddListener(gameManager.UpdateScoreOnSplitBall);
            // subscribe to upgrade handlers laser upgrade
            upgradeHandler.OnLasersUpgraded.AddListener(UnSubscribeFromReturningToPool);
        }
        private void UnSubscribeFromReturningToPool()
        {
            OnHitBall.RemoveListener(ReturnSelfToPool);
        }
        public void ReturnSelfToPool()//returning too object pool by set game object off
        {
            this.gameObject.SetActive(false);

        }
        public void ReturnSelfToPool(ILaserHandler laser, Ball ball)//returning too object pool by set game object off/ overload
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
                OnHitBall?.Invoke(this, collision.gameObject.GetComponent<Ball>());
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
