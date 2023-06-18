using UnityEngine;
namespace model
{
    [CreateAssetMenu(fileName = "LevelConfigList", menuName = "Pang/Level Config List", order = 11)]
    public class LevelConfigList : ScriptableObject//SO that holds Level data
    {
      [SerializeField] private LevelConfig[] levelConfigs;
      public LevelConfig[] LevelConfigs { get => levelConfigs; }
    }
}