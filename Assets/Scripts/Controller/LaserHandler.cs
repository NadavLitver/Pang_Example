using UnityEngine;
using UnityEngine.Events;
using model;
namespace controller
{
    public class LaserHandler : MonoBehaviour// the laser handler handels individual projectile logic
    {
        public UnityEvent<LaserHandler, Rigidbody2D> onHitBall;
        public LaserConfig laserData;
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
            transform.Translate(laserData.Speed * Vector2.up * Time.deltaTime);

            CheckTimeToLive();

        }

        private void CheckTimeToLive()
        {
            currentTimeAlive += Time.deltaTime;
            if (currentTimeAlive > laserData.TimeToLive)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
