using UnityEngine;
using UnityEngine.Events;
namespace controller
{
    public interface IGameManager
    {
        UnityEvent OnLose { get; }
        float Score { get; }
        void CheckLose(int currentHealthPoints);
        void UpdateScoreOnSplitBall(ILaserHandler laser, Rigidbody2D rigidbody2D);
        void ResetScene();
        void ReduceScoreOnHit(int healthPoints);
        
    }
}