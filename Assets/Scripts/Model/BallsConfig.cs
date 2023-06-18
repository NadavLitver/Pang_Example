using System.Collections.Generic;
using UnityEngine;
namespace model
{
    [CreateAssetMenu(fileName = "BallsConfig", menuName = "Pang/BallsDatas Config", order = 2)]
    public class BallsConfig : ScriptableObject//SO that holds ball data
    {
        [SerializeField] private float bounceForce;
        [SerializeField] private LayerMask collisionLayer;
        public LayerMask CollisionLayer { get => collisionLayer; }

        public float BounceForce { get => bounceForce; }

        [SerializeField] private float[] ballSizes;
        public float[] BallSizes { get => ballSizes;}

     



    }
}