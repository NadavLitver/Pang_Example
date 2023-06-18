using Codice.CM.Common;
using model;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace controller
{
    public class Ball : MonoBehaviour,IBall// ball handles movement and collision
    {
        //events
        public UnityEvent OnPlayerHit { get; } = new UnityEvent();
        //controllers
        [Inject] private readonly IPlayerHPHandler playerHPHandler;
        [Inject] private readonly BallsConfig ballsConfig;
        public Rigidbody2D Rb2d { get => rb2d;}

        public BallData ballData { get; set; }
        [SerializeField] Rigidbody2D rb2d;
      
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
        private void Update()
        {
            CheckCollisionForBall();
        }
        private void CheckCollisionForBall()
        {
            Vector2 scale = transform.localScale;

            // Calculate the raycast size based on the ball's scale
            float raycastSize = scale.x * 0.8f;
            //Draw debugs
            Debug.DrawRay(transform.position, Vector2.left * raycastSize, Color.red);
            Debug.DrawRay(transform.position, Vector2.right * raycastSize, Color.green);
            Debug.DrawRay(transform.position, Vector2.down * raycastSize, Color.blue);
            // Perform raycast checks on the bottom-left, bottom-right, and bottom of the ball
            bool isLeftHit;
            bool isRightHit;
            bool isBottomHit;
            isLeftHit = Physics2D.Raycast(transform.position, Vector2.left, raycastSize, ballsConfig.CollisionLayer);
            if (isLeftHit)
            {
                Rb2d.velocity = new Vector2(1 * ballData.Speed, Rb2d.velocity.y); // Process collision results as needed
                return;
            }
            isRightHit = Physics2D.Raycast(transform.position, Vector2.right, raycastSize, ballsConfig.CollisionLayer);
            if (isRightHit)
            {
                Rb2d.velocity = new Vector2(-1 * ballData.Speed, Rb2d.velocity.y); // Process collision results as needed
                return;
            }
            isBottomHit = Physics2D.Raycast(transform.position, Vector2.down, raycastSize, ballsConfig.CollisionLayer);
            if (isBottomHit)
            { // Process collision results as needed

                if (Rb2d.velocity.x > 0)
                {
                    Rb2d.velocity = new Vector2(1 * ballData.Speed, ballsConfig.BounceForce);
                }
                else
                {
                    Rb2d.velocity = new Vector2(-1 * ballData.Speed, ballsConfig.BounceForce);
                }
                return;
            }
        }

        public void ReturnSelfToPool()
        {
            this.gameObject.SetActive(false);
        }
    }
   

    
}