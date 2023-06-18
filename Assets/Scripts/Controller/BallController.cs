using model;
using System.Collections.Generic;
using UnityEngine;
using view;
using Zenject;

namespace controller
{
    public class BallController : IBallController, ITickable // BallController: movement and collision logic for the balls.
    {
        //controllers
        private readonly IBallsPoolHandler ballPoolHandler;
        //view
        private readonly ISoundManager soundManager;
        //data
        private readonly BallsConfig ballsData;
        [Inject]
        public BallController(ISoundManager _soundManager, IBallsPoolHandler _ballDataHandler, BallsConfig _ballsData)
        {
            this.soundManager = _soundManager;
            this.ballPoolHandler = _ballDataHandler;
            this.ballsData = _ballsData;
        }


        //  method for ball creation
        public void CreateBall()//default overload
        {
            Ball ball = ballPoolHandler.BallPoolRef.GetFromPool();
            ball.transform.position = Vector3.zero;
            ball.transform.localScale = Vector3.one;
            Rigidbody2D ballRB = ball.Rb2d;
            ballRB.velocity = RandomBallVelocity();
            ballPoolHandler.ActiveBalls.Add(ball);
            // Set ball position, velocity, and any other necessary properties
        }


        public void CreateBall(Vector2 pos, Vector2 scale, Vector2 velocity)//overloaded 
        {
            Ball ball = ballPoolHandler.BallPoolRef.GetFromPool();
            ball.transform.position = pos;
            ball.transform.localScale = scale;
            Rigidbody2D ballRB = ball.Rb2d;
            ballRB.velocity = velocity;
            ballPoolHandler.ActiveBalls.Add(ball);
            // Set ball position, velocity, and any other necessary properties
        }

        private void ReturnBallToPool(Ball ball) //  method for returning a ball to the pool(disabling it)
        {
            RemoveFromActiveBalls(ball);

            ballPoolHandler.BallPoolRef.ReturnToPool(ball);
        }

        public Vector2 RandomBallVelocity()// get a random velocity from a ball
        {
            return GetRandomRightOrLeft() * ballsData.Speed;
        }
        private void RemoveFromActiveBalls(Ball ball)//Remove ball from the Active balls list
        {
            int index = ballPoolHandler.ActiveBalls.IndexOf(ball);

            if (index != -1)// if indexOf can't find it return -1 so incase it found something we continue to the rest of the logic
            {
                int lastIndex = ballPoolHandler.ActiveBalls.Count - 1;

                // Swap the ball to be removed with the last ball in the list
                Ball lastBall = ballPoolHandler.ActiveBalls[lastIndex];
                ballPoolHandler.ActiveBalls[lastIndex] = ball;
                ballPoolHandler.ActiveBalls[index] = lastBall;
                ballPoolHandler.ActiveBalls.RemoveAt(lastIndex);
            }
        }


        private void CheckCollisionForBall(Ball ball)
        {
            Vector2 scale = ball.transform.localScale;

            // Calculate the raycast size based on the ball's scale
            float raycastSize = scale.x * 0.8f;
            //Draw debugs
            Debug.DrawRay(ball.transform.position, Vector2.left * raycastSize, Color.red);
            Debug.DrawRay(ball.transform.position, Vector2.right * raycastSize, Color.green);
            Debug.DrawRay(ball.transform.position, Vector2.down * raycastSize, Color.blue);
            // Perform raycast checks on the bottom-left, bottom-right, and bottom of the ball
            bool isLeftHit;
            bool isRightHit;
            bool isBottomHit;
            isLeftHit = Physics2D.Raycast(ball.transform.position, Vector2.left, raycastSize, ballsData.CollisionLayer);
            if(isLeftHit)
            {
                ball.Rb2d.velocity = new Vector2(1 * ballsData.Speed, ball.Rb2d.velocity.y); // Process collision results as needed
                Debug.Log("LeftHit");
                return;
            }
            isRightHit = Physics2D.Raycast(ball.transform.position, Vector2.right, raycastSize, ballsData.CollisionLayer);
            if(isRightHit)
            {
                ball.Rb2d.velocity = new Vector2(-1 * ballsData.Speed, ball.Rb2d.velocity.y); // Process collision results as needed
                Debug.Log("RightHit");

                return;
            }
            isBottomHit = Physics2D.Raycast(ball.transform.position, Vector2.down, raycastSize, ballsData.CollisionLayer);
            if (isBottomHit)
            { // Process collision results as needed
                Debug.Log("BottomHit");

                if (ball.Rb2d.velocity.x > 0)
                {
                    ball.Rb2d.velocity = new Vector2(1 * ballsData.Speed, ballsData.BounceForce);
                }
                else
                {
                    ball.Rb2d.velocity = new Vector2(-1 * ballsData.Speed, ballsData.BounceForce);
                }
                return;
            }
      
        }
        public void Tick()
        {
            IterateOverBallsAndCheckCollision();
        }


        private void IterateOverBallsAndCheckCollision()// loop over all active balls and check collision
        {
            foreach (var ball in ballPoolHandler.ActiveBalls)//while a for loop might have been slightly faster the differences are negligible and a foreach loop is more readable and I don't need the index
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
        public void SplitBall(ILaserHandler laser, Ball ball)// split ball depending on scale
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
            return ballPoolHandler.ActiveBalls.Count == 0;
        }
        private void CreateTwoBalls(Ball ball, float size)// create to balls that go to different direction in the position of a given ball
        {
            CreateBall(ball.transform.position, Vector2.one * size, Vector2.right * ballsData.Speed);
            CreateBall(ball.transform.position, Vector2.one * size, Vector2.left * ballsData.Speed);
        }

        [ContextMenu("SplitBalls")]
        private void SplitAllBalls()//just for testing in editor
        {
            List<Ball> ballsToSplit = new List<Ball>(); // Create a separate list to store the balls to be split

            foreach (var ball in ballPoolHandler.ActiveBalls)
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