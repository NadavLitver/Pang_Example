using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace model
{
    public class BallsPoolHandler : MonoBehaviour
    {
        [Inject(Id = "BallPool")] IObjectPool ballPoolRef;
       // [SerializeField] PlayerHPHandler playerHPHandler;
        List<Rigidbody2D> activeBalls;

        public IObjectPool BallPoolRef { get => ballPoolRef;}
      
        public List<Rigidbody2D> ActiveBalls { get => activeBalls; }
        

        private void Awake()
        {
            activeBalls = new List<Rigidbody2D>();
        }
       
    }
}