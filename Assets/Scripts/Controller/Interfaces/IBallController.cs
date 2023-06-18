using UnityEngine;
namespace controller
{
    public interface IBallController
    {
        void CreateBall();
        void CreateBall(Vector2 pos, Vector2 scale, Vector2 velocity);
        void SplitBall(ILaserHandler laser, Rigidbody2D ball);
        bool IsActiveBallsEmpty();
        Vector2 RandomBallVelocity();
    }
}