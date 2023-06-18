using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace controller
{
    public class BallsPoolHandler : IBallsPoolHandler//class resposible for holding current active balls
    {
        [Inject]private readonly IObjectPool<Ball> ballPoolRef;

        List<IBall> activeBalls = new List<IBall>();

        public IObjectPool<Ball> BallPoolRef { get => ballPoolRef;}
   
        public List<IBall> ActiveBalls { get => activeBalls; }
       
    }
}