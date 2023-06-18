using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace controller
{
    public class Ball : MonoBehaviour// ball only exists to raise event when a ball collides with the player
    {
        //events
        public UnityEvent OnPlayerHit = new UnityEvent();
        //controllers
        [Inject] private readonly IPlayerHPHandler playerHPHandler;
        [SerializeField] Rigidbody2D rb2d;

        public Rigidbody2D Rb2d { get => rb2d;}

        private void Start()
        {
            OnPlayerHit.AddListener(playerHPHandler.PlayerHit);
        }

        //identify collision with player and invoke event
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