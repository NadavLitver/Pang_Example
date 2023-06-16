using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace model
{
    public class BallsPoolHandler : IBallsPoolHandler
    {
        [Inject(Id = "BallPool")] IObjectPool ballPoolRef;
       // [SerializeField] PlayerHPHandler playerHPHandler;
        List<Rigidbody2D> activeBalls = new List<Rigidbody2D>();

        public IObjectPool BallPoolRef { get => ballPoolRef;}
        
        public List<Rigidbody2D> ActiveBalls { get => activeBalls; }
       
    }
}