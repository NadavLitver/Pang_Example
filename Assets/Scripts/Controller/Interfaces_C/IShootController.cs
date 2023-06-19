using UnityEngine;
using System;
namespace controller
{
    public interface IShootController
    {
        Action OnShot { get; set; }
    }
}