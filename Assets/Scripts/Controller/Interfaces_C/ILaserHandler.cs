using model;
using System;
using UnityEngine;
using UnityEngine.Events;
namespace controller
{
    public interface ILaserHandler
    {
        Action<ILaserHandler, IBall> OnHitBall { get; set; }
        void ReturnSelfToPool(ILaserHandler laser, IBall ball);
        void CheckTimeToLive();
        GameObject myGameObject { get; }
    }
}