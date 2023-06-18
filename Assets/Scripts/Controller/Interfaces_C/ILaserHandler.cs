using model;
using UnityEngine;
using UnityEngine.Events;
namespace controller
{
    public interface ILaserHandler
    {
        UnityEvent<ILaserHandler, Ball> OnHitBall { get; }
        void ReturnSelfToPool(ILaserHandler laser, Ball ball);
        void CheckTimeToLive();
        GameObject myGameObject { get; }
    }
}