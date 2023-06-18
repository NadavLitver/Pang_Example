using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace controller
{
    public class BallsPoolHandler : IBallsPoolHandler//class resposible for holding current active balls
    {
        [Inject]private readonly IObjectPool<Ball> ballPoolRef;

        List<Ball> activeBalls = new List<Ball>();

        public IObjectPool<Ball> BallPoolRef { get => ballPoolRef;}
        
        public List<Ball> ActiveBalls { get => activeBalls; }
       
    }
}