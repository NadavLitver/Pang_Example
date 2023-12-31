using UnityEngine;
namespace model
{
    [CreateAssetMenu(fileName = "LevelConfigList", menuName = "Pang/Level Config List", order = 11)]
    public class LevelConfigList : ScriptableObject,ILevelConfigList//SO that holds Level data
    {
      [SerializeField] private LevelConfig[] levelConfigs;
      public ILevelConfig[] LevelConfigs { get => levelConfigs; }
    }
}