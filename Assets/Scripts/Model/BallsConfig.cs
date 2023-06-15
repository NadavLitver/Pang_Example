using UnityEngine;
namespace model
{
    [CreateAssetMenu(fileName = "BallsConfig", menuName = "Pang/Balls Config", order = 2)]
    public class BallsConfig : ScriptableObject
    {
        public float speed;
        public float BounceForce;
    }
}