using model;
using UnityEngine;
using UnityEngine.Events;
namespace controller
{
    public interface ILaserHandler
    {
        UnityEvent<ILaserHandler, Rigidbody2D> OnHitBall { get; }
        
        void CheckTimeToLive();
        GameObject myGameObject { get; }
    }
}