using UnityEngine;
namespace controller
{
    public interface IBallController
    {
        IBall CreateBall();
        IBall CreateBall(Vector2 pos, Vector2 scale, Vector2 velocity);
        IBall CreateBall(IBall ball);

        void SplitBall(ILaserHandler laser, IBall ball);
        bool IsActiveBallsEmpty();
        Vector2 RandomBallVelocity();
        Vector2 RandomBallVelocity(float speed);

    }
}