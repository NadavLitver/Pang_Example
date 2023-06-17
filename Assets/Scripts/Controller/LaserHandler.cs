using UnityEngine;
using UnityEngine.Events;
using model;
namespace controller
{
    public class LaserHandler : MonoBehaviour, ILaserHandler// the laser handler handels individual projectile logic
    {
        public UnityEvent<ILaserHandler, Rigidbody2D> OnHitBall { get; private set; }
        public LaserConfig LaserData;

        public GameObject myGameObject => gameObject;

        private float currentTimeAlive;
        private void Awake()
        {
            OnHitBall = new UnityEvent<ILaserHandler, Rigidbody2D>();
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
