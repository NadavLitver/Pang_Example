using System.Collections.Generic;
using UnityEngine;
namespace model
{
    public interface IBallsPoolHandler
    {
        IObjectPool BallPoolRef { get; }
        List<Rigidbody2D> ActiveBalls { get; }
    }
}