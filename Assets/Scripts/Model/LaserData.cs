
using UnityEngine;
namespace model
{

    public class LaserData : MonoBehaviour
    {
        [SerializeField] LaserConfig config;
        [SerializeField] ObjectPool laserPool;
        public float Speed { get => config.speed; }
        public float TTL { get => config.timeToLive; }
        public ObjectPool LaserPool { get => laserPool; }
    }
}