using UnityEngine;
namespace model
{
    [CreateAssetMenu(fileName = "BallsConfig", menuName = "Pang/BallsDatas Config", order = 2)]
    public class BallsConfig : ScriptableObject,IBallsConfig//SO that holds ball data
    {
        [SerializeField] private float bounceForce;
        [SerializeField] private LayerMask collisionLayer;
        [SerializeField] private float[] ballSizes;
        public LayerMask CollisionLayer { get => collisionLayer; }

        public float BounceForce { get => bounceForce; }

        public float[] BallSizes { get => ballSizes; }





    }
}