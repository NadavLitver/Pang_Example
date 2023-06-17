using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace model
{
    public class BallsPoolHandler : IBallsPoolHandler
    {
        private readonly IObjectPool ballPoolRef;

        List<Rigidbody2D> activeBalls = new List<Rigidbody2D>();

        public IObjectPool BallPoolRef { get => ballPoolRef;}
        
        public List<Rigidbody2D> ActiveBalls { get => activeBalls; }

        public BallsPoolHandler([Inject(Id = "BallPool")] IObjectPool _ballPoolRef)
        {
            ballPoolRef = _ballPoolRef;
            ballPoolRef.PopulatePool();
        }
       
    }
}