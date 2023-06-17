using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace controller
{
    public class Ball : MonoBehaviour// ball only exists to raise event when a ball collides with the player
    {
        public UnityEvent OnPlayerHit;
        [Inject] private readonly IPlayerHPHandler playerHPHandler;
       
        private void Start()
        {
            OnPlayerHit.AddListener(playerHPHandler.PlayerHit);
        }

     
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