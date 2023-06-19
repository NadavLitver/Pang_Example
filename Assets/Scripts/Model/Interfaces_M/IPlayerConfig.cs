using UnityEngine;

namespace model
{
    public interface IPlayerConfig
    {
        float ShootCD { get; }
        float HitCD { get; }
        float MoveSpeed { get; }
        float Width { get; }
        int StartingHP { get; }
        LayerMask CollisionLayer { get; }
    }
}