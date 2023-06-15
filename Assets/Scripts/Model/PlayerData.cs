using UnityEngine;
namespace model
{
    public class PlayerData : MonoBehaviour
    {
        [SerializeField] PlayerConfig playerConfig;

        public float Speed { get => playerConfig.moveSpeed; }
        public float ShootCD { get => playerConfig.shootCD; }
        public float HitCD { get => playerConfig.hitCD; }

        public int StartHP { get => playerConfig.StartingHP; }
        public float width { get => playerConfig.Width; }

        public LayerMask collisionMask { get => playerConfig.collisionLayer; }

    }
}