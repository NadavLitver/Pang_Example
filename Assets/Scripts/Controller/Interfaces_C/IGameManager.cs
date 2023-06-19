using System;
namespace controller
{
    public interface IGameManager
    {
        Action OnLose { get; set; }
        float Score { get; }
        void CheckLose(int currentHealthPoints);
        void UpdateScoreOnSplitBall(ILaserHandler laser, IBall ball);
        void ReduceScoreOnHit(int healthPoints);
        
    }
}