using System.Collections.Generic;
using UnityEngine;
namespace model
{
    [CreateAssetMenu(fileName = "BallsConfig", menuName = "Pang/Balls Config", order = 2)]
    public class BallsConfig : ScriptableObject//SO that holds ball data
    {
        [SerializeField] private float speed;
        [SerializeField] private float bounceForce;
        public LayerMask CollisionLayer;

        public float Speed { get => speed; }
        public float BounceForce { get => bounceForce; }

        [SerializeField] private float[] ballSizes;
        public float[] BallSizes { get => ballSizes;}

     



    }
}