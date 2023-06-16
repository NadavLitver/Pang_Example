using UnityEngine;
using UnityEngine.Events;
namespace controller
{
    public interface IGameManager
    {
        UnityEvent OnLose { get; }
        UnityEvent<bool> OnEnd { get; }
        float Score { get; }
        void CheckLose(int currentHealthPoints);
        void UpdateScoreOnSplitBall(ILaserHandler laser, Rigidbody2D ball);
        void ResetScene();
        void ReduceScore(float scoreToDeduct);
        void AddHealthPointsAndUpdateUI(int healthPoints);
    }
}