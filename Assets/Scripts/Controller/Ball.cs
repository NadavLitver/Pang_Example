using UnityEngine;
using UnityEngine.Events;
namespace controller
{
    public class Ball : MonoBehaviour// ball only exists to raise event when a ball collides with the player
    {
        public UnityEvent OnPlayerHit;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                OnPlayerHit?.Invoke();

                Debug.Log("PlayerHit");
            }
        }
    }
}