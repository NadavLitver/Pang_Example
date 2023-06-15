using UnityEngine;
namespace model
{
    [CreateAssetMenu(fileName = "LaserConfig", menuName = "Pang/Laser Config", order = 4)]

    public class LaserConfig : ScriptableObject
    {
        [SerializeField] private float speed;
        [SerializeField] private float timeToLive;//time to live

        public float Speed { get => speed;}
        public float TimeToLive { get => timeToLive;}
    }
}