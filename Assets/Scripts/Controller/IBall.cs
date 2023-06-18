using UnityEngine.Events;
using UnityEngine;
using controller;
using model;

public interface IBall
{
    Rigidbody2D Rb2d { get; }
    UnityEvent OnPlayerHit { get; }
    void ReturnSelfToPool();
    BallData ballData { get; set; }
}