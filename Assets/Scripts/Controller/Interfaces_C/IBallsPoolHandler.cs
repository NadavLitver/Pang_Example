using System.Collections.Generic;
using UnityEngine;
namespace controller
{
    public interface IBallsPoolHandler
    {
        IObjectPool<Ball> BallPoolRef { get; }
        List<IBall> ActiveBalls { get; }
    }
}