using controller;
using UnityEngine.Events;
using UnityEngine;

public interface IShootController
{
    UnityEvent onShot { get; }
    void ReturnLaser(LaserHandler laserHandler, Rigidbody2D ballHit);
}