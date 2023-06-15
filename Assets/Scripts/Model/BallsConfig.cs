using UnityEngine;
namespace model
{
    [CreateAssetMenu(fileName = "BallsConfig", menuName = "Pang/Balls Config", order = 2)]
    public class BallsConfig : ScriptableObject
    {
        [SerializeField] private float speed;
        [SerializeField] private float bounceForce;
        public LayerMask CollisionLayer;

        public float Speed { get => speed; }
        public float BounceForce { get => bounceForce;  }
    }
}