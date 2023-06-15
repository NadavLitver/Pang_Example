using UnityEngine;
[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Pang/Player Config", order = 3)]

public class PlayerConfig : ScriptableObject
{
    public float shootCD;
    public float hitCD;
    public float moveSpeed;
    public float Width;
    public int StartingHP;
    public LayerMask collisionLayer;
}
