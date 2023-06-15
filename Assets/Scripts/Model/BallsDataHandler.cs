using System.Collections.Generic;
using UnityEngine;
using controller;
namespace model
{
    public class BallsDataHandler : MonoBehaviour
    {
        [SerializeField] ObjectPool ballPoolRef;
        [SerializeField] BallsConfig ballsData;
        [SerializeField] PlayerHPHandler playerHPHandler;
        List<Rigidbody2D> activeBalls;
        public LayerMask collisionLayer;

        public ObjectPool BallPoolRef { get => ballPoolRef;}
        internal float Speed { get => ballsData.speed; }
        internal List<Rigidbody2D> ActiveBalls { get => activeBalls; }
        public float BounceForce { get => ballsData.BounceForce;}

        private void Awake()
        {
            activeBalls = new List<Rigidbody2D>();
        }
        private void Start()
        {
            foreach (var currentBall in ballPoolRef.Pool)
            {
              Ball currentBallRef = currentBall.GetComponent<Ball>();
              currentBallRef.OnPlayerHit.AddListener(playerHPHandler.PlayerHit);
            }
        }
    }
}