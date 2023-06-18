using model;
using UnityEngine;
using UnityEngine.Events;
namespace controller
{
    public interface ILaserHandler
    {
        UnityEvent<ILaserHandler, IBall> OnHitBall { get; }
        void ReturnSelfToPool(ILaserHandler laser, IBall ball);
        void CheckTimeToLive();
        GameObject myGameObject { get; }
    }
}