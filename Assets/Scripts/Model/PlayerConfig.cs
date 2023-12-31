using UnityEngine;
namespace model
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Pang/Player Config", order = 3)]

    public class PlayerConfig : ScriptableObject,IPlayerConfig//SO that holds player data
    {
        [SerializeField] private float shootCD;
        [SerializeField] private float hitCD;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float width;
        [SerializeField] private int startingHP;

        [SerializeField] LayerMask collisionLayer;

        public float ShootCD { get => shootCD;}
        public float HitCD { get => hitCD; }
        public float MoveSpeed { get => moveSpeed;}
        public float Width { get => width;}
        public int StartingHP { get => startingHP; }
        public LayerMask CollisionLayer { get => collisionLayer; }
}
}