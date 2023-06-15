using UnityEngine;
using UnityEngine.Events;
namespace model
{
    public class LaserHandler : MonoBehaviour// the laser handler handels individual projectile logic
    {
        public UnityEvent<LaserHandler, Rigidbody2D> onHitBall;
        [SerializeField] float speed;
        [SerializeField] float timeToLive;//time to live
        private float currentTimeAlive;
        private void OnEnable()
        {
            currentTimeAlive = 0;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Ball"))
            {
                onHitBall?.Invoke(this, collision.gameObject.GetComponent<Rigidbody2D>());
            }
        }
        private void Update()
        {
            transform.Translate(speed * Vector2.up * Time.deltaTime);

            CheckTimeToLive();

        }

        private void CheckTimeToLive()
        {
            currentTimeAlive += Time.deltaTime;
            if (currentTimeAlive > timeToLive)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
