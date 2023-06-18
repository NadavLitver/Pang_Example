using model;
using ModestTree;
using UnityEngine;
using view;
using Zenject;
using static UnityEngine.UIElements.UxmlAttributeDescription;

namespace controller
{
    public class BallController : IBallController // BallController: General Functions For balls, Creation and Pool methods for ball
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


      
        //method for creating ball used when creating new balls 
        public IBall CreateBall(Vector2 pos, Vector2 scale, Vector2 velocity)
        {
            Ball ball = ballPoolHandler.BallPoolRef.GetFromPool();
            ball.Rb2d.transform.position = pos;
            ball.Rb2d.transform.localScale = scale;
            Rigidbody2D ballRB = ball.Rb2d;
            ballRB.velocity = velocity;
            ballPoolHandler.ActiveBalls.Add(ball);
            return ball;
            // Set ball position, velocity, and any other necessary properties
        }
        //overload method used when splitting balls
        public IBall CreateBall(IBall fatherBall, Vector2 dir)
        {
            Ball ball = ballPoolHandler.BallPoolRef.GetFromPool();
            ball.ballData = fatherBall.ballData.ChildData;
            ball.Rb2d.transform.position = fatherBall.Rb2d.transform.position;
            ball.Rb2d.transform.localScale = ball.ballData.Size * Vector2.one;
            Rigidbody2D ballRB = ball.Rb2d;
            ballRB.velocity = dir * ball.ballData.Speed;
            ballPoolHandler.ActiveBalls.Add(ball);
            return ball;
        }
        private void ReturnBallToPool(IBall ball) //  method for returning a ball to the pool(disabling it)
        {
            RemoveFromActiveBalls(ball);

            ballPoolHandler.BallPoolRef.ReturnToPool((Ball)ball);
        }

        public Vector2 RandomBallVelocity()// get a random velocity from a ball with default speed
        {
            return GetRandomRightOrLeft() * ballsData.Speed;
        }
        public Vector2 RandomBallVelocity(float speed)// get a random velocity from a ball with speed overload
        {
            return GetRandomRightOrLeft() * speed;
        }
        private void RemoveFromActiveBalls(IBall ball)//Remove ball from the Active balls list
        {
            int index = ballPoolHandler.ActiveBalls.IndexOf(ball);

            if (index != -1)// if indexOf can't find it return -1 so incase it found something we continue to the rest of the logic
            {
                int lastIndex = ballPoolHandler.ActiveBalls.Count - 1;

                // Swap the ball to be removed with the last ball in the list
                IBall lastBall = ballPoolHandler.ActiveBalls[lastIndex];
                ballPoolHandler.ActiveBalls[lastIndex] = ball;
                ballPoolHandler.ActiveBalls[index] = lastBall;
                ballPoolHandler.ActiveBalls.RemoveAt(lastIndex);
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
        public void SplitBall(ILaserHandler laser, IBall ball)// split ball depending on scale
        {
            float currentBallScale = ball.Rb2d.gameObject.transform.localScale.x;// get current size
            if (ball.ballData.SplitAmount != 0 && GetIndexOfSize(currentBallScale) != 0)//check ball isnt the smallest size and has split amount
            {
                Split(ball);// create two balls one size smaller
            }
            ReturnBallToPool(ball);//return ball to pool
            soundManager.Play(SoundManager.Sound.ballHit);

        }
        public bool IsActiveBallsEmpty()// check if there are any active balls left
        {
            return ballPoolHandler.ActiveBalls.Count == 0;
        }
        private void Split(IBall ball)// create to balls that go to different direction in the position of a given ball
        {
            bool isRight = true;
            for (int i = 0; i < ball.ballData.SplitAmount; i++)
            {
                Vector2 dir = isRight ? Vector2.right : Vector2.left;//make sure balls wont go to same direction
                isRight = !isRight;//flip for next loop
                CreateBall(ball, dir);
            }

        }


        public float GetSmallerSize(float size)
        {
            int index = GetIndexOfSize(size);
            if (index > 0 && index < ballsData.BallSizes.Length)
            {
                return ballsData.BallSizes[index - 1];
            }
            else
            {
                // Handle the case when the size is not found or it is the smallest size
                return ballsData.BallSizes[0];
            }
        }
        public int GetIndexOfSize(float size)
        {
            return ballsData.BallSizes.IndexOf(size);
        }
        public float GetSizeUsingIndex(int index)
        {
            if (index > 0 && index < ballsData.BallSizes.Length)
            {
                return ballsData.BallSizes[index];
            }
            else
            {
                // Handle the case when the size is not found or it is the smallest size
                return ballsData.BallSizes[0];
            }
        }


    }
}