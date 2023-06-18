using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace model
{
    public class BallsPoolHandler : IBallsPoolHandler//class resposible for holding current active balls
    {
        [Inject(Id = "BallPool")] private readonly IObjectPool ballPoolRef;

        List<Rigidbody2D> activeBalls = new List<Rigidbody2D>();

        public IObjectPool BallPoolRef { get => ballPoolRef;}
        
        public List<Rigidbody2D> ActiveBalls { get => activeBalls; }
       
    }
}