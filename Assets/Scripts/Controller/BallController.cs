using model;
using System.Collections.Generic;
using UnityEngine;
using view;
using Zenject;

namespace controller
{
    [DefaultExecutionOrder(+1)]//delay execution
    public class BallController : MonoBehaviour, IBallController // BallController: movement and collision logic for the balls.
    {

        //view
        [Inject] private ISoundManager soundManager;
        //data
        [Inject] private IBallsPoolHandler ballDataHandler;
        [SerializeField] private BallsConfig ballsData;

        //  method for ball creation
        public void CreateBall()//default overload
        {
            GameObject ball = ballDataHandler.BallPoolRef.GetFromPool();
            ball.transform.position = Vector3.zero;
            ball.transform.localScale = Vector3.one;
            Rigidbody2D ballRB = ball.GetComponent<Rigidbody2D>();
            ballRB.velocity = RandomBallVelocity();
            ballDataHandler.ActiveBalls.Add(ballRB);
            // Set ball position, velocity, and any other necessary properties
        }


        public void CreateBall(Vector2 pos, Vector2 scale, Vector2 velocity)//overloaded 
        {
            GameObject ball = ballDataHandler.BallPoolRef.GetFromPool();
            ball.transform.position = pos;
            ball.transform.localScale = scale;
            Rigidbody2D ballRB = ball.GetComponent<Rigidbody2D>();
            ballRB.velocity = velocity;
            ballDataHandler.ActiveBalls.Add(ballRB);
            // Set ball position, velocity, and any other necessary properties
        }
       
        private void ReturnBallToPool(Rigidbody2D ball) //  method for returning a ball to the pool(disabling it)
        {
            RemoveFromActiveBalls(ball);

            ballDataHandler.BallPoolRef.ReturnToPool(ball.gameObject);
        }

        public Vector2 RandomBallVelocity()// get a random velocity from a ball
        {
            return GetRandomRightOrLeft() * ballsData.Speed;
        }
        private void RemoveFromActiveBalls(Rigidbody2D ball)//Remove ball from the Active balls list
        {
            int index = ballDataHandler.ActiveBalls.IndexOf(ball);

            if (index != -1)// if indexOf can't find it return -1 so incase it found something we continue to the rest of the logic
            {
                int lastIndex = ballDataHandler.ActiveBalls.Count - 1;

                // Swap the ball to be removed with the last ball in the list
                Rigidbody2D lastBall = ballDataHandler.ActiveBalls[lastIndex];
                ballDataHandler.ActiveBalls[lastIndex] = ball;
                ballDataHandler.ActiveBalls[index] = lastBall;
                ballDataHandler.ActiveBalls.RemoveAt(lastIndex);
            }
        }


        private void CheckCollisionForBall(Rigidbody2D ball)
        {
            Vector2 scale = ball.transform.localScale;

            // Calculate the raycast size based on the ball's scale
            float raycastSize = scale.x * 0.8f;

            // Perform raycast checks on the bottom-left, bottom-right, and bottom of the ball
            bool isLeftHit = Physics2D.Raycast(ball.transform.position, Vector2.left, raycastSize, ballsData.CollisionLayer);
            bool isRightHit = Physics2D.Raycast(ball.transform.position, Vector2.right, raycastSize, ballsData.CollisionLayer);
            bool isBottomHit = Physics2D.Raycast(ball.transform.position, Vector2.down, raycastSize, ballsData.CollisionLayer);

            //Draw debugs
            Debug.DrawRay(ball.transform.position, Vector2.left * raycastSize, Color.red);
            Debug.DrawRay(ball.transform.position, Vector2.right * raycastSize, Color.green);
            Debug.DrawRay(ball.transform.position, Vector2.down * raycastSize, Color.blue);

            // Process collision results as needed
            if (isLeftHit)
            {
                ball.velocity = new Vector2(1 * ballsData.Speed, ball.velocity.y);
            }
            else if (isRightHit)
            {
                ball.velocity = new Vector2(-1 * ballsData.Speed, ball.velocity.y);
            }
            else if (isBottomHit)
            {

                if (ball.velocity.x > 0)
                {
                    ball.velocity = new Vector2(1 * ballsData.Speed, ballsData.BounceForce);
                }
                else
                {
                    ball.velocity = new Vector2(-1 * ballsData.Speed, ballsData.BounceForce);
                }

            }
        }
        private void Update()
        {
            IterateOverBallsAndCheckCollision();
        }

        private void IterateOverBallsAndCheckCollision()// loop over all active balls and check collision
        {
            foreach (var ball in ballDataHandler.ActiveBalls)//while a for loop might have been slightly faster the differences are negligible and a foreach loop is more readable and I don't need the index
            {
                CheckCollisionForBall(ball);
            }
        }

        private Vector2 GetRandomRightOrLeft()
        {
            // Generate a random value (0 or 1) to determine the direction
            int randomValue = Random.Range(0, 2);

            // Conditionally assign the appropriate vector based on the random value
            Vector2 randomDirection = (randomValue == 0) ? Vector2.right : Vector2.left;

            return randomDirection;
        }
        public void SplitBall(ILaserHandler laser, Rigidbody2D ball)// split ball depending on scale
        {
            float currentBallScale = ball.gameObject.transform.localScale.x;
            switch (currentBallScale)
            {
                case 3:
                    CreateTwoBalls(ball, 2);
                    break;
                case 2:
                    CreateTwoBalls(ball, 1);
                    break;
                case 1:
                    CreateTwoBalls(ball, 0.5f);
                    break;
                case 0.5f:
                    CreateTwoBalls(ball, 0.35f);
                    break;
                case 0.35f:
                    break;
                default:
                    break;
            }
            ReturnBallToPool(ball);
            soundManager.Play(SoundManager.Sound.ballHit);

        }
        public bool IsActiveBallsEmpty()// check if there are any active balls left
        {
            return ballDataHandler.ActiveBalls.Count == 0;
        }
        private void CreateTwoBalls(Rigidbody2D ball, float size)// create to balls that go to different direction in the position of a given ball
        {
            CreateBall(ball.transform.position, Vector2.one * size, Vector2.right * ballsData.Speed);
            CreateBall(ball.transform.position, Vector2.one * size, Vector2.left * ballsData.Speed);
        }

        [ContextMenu("SplitBalls")]
        private void SplitAllBalls()//just for testing in editor
        {
            List<Rigidbody2D> ballsToSplit = new List<Rigidbody2D>(); // Create a separate list to store the balls to be split

            foreach (var ball in ballDataHandler.ActiveBalls)
            {
                ballsToSplit.Add(ball); // Add the ball to the separate list
            }

            foreach (var ball in ballsToSplit) // Iterate over the separate list
            {
                float currentBallScale = ball.gameObject.transform.localScale.x;
                switch (currentBallScale)
                {
                    case 3:
                        CreateTwoBalls(ball, 2);
                        break;
                    case 2:
                        CreateTwoBalls(ball, 1);
                        break;
                    case 1:
                        CreateTwoBalls(ball, 0.5f);
                        break;
                    case 0.5f:
                        CreateTwoBalls(ball, 0.35f);
                        break;
                    case 0.35f:
                        break;
                    default:
                        break;
                }
                ReturnBallToPool(ball);
            }
        }


    }
}