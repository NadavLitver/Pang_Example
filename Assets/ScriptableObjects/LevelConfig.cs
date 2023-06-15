using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Pang/Level Config", order = 1)]
public class LevelConfig : ScriptableObject
{
    public int levelIndex;
    public int ballCount;
    public float ballSize;
}