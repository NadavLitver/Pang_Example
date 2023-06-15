using System.Collections.Generic;
using UnityEngine;

namespace model
{
    public class BallsPoolHandler : MonoBehaviour
    {
        [SerializeField] ObjectPool ballPoolRef;
       // [SerializeField] PlayerHPHandler playerHPHandler;
        List<Rigidbody2D> activeBalls;

        public ObjectPool BallPoolRef { get => ballPoolRef;}
      
        public List<Rigidbody2D> ActiveBalls { get => activeBalls; }
        

        private void Awake()
        {
            activeBalls = new List<Rigidbody2D>();
        }
       
    }
}