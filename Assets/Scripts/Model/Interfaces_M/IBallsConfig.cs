using UnityEngine;

namespace model
{
    public interface IBallsConfig
    {
        float BounceForce { get; }
        LayerMask CollisionLayer { get; }
        float[] BallSizes { get; }
    }
}