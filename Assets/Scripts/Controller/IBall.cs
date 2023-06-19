using model;
using System;
using UnityEngine;
namespace controller
{
    public interface IBall
    {
        Rigidbody2D Rb2d { get; }
        Action OnPlayerHit { get; set; }
        void ReturnSelfToPool();
        IBallData ballData { get; set; }
    }
}