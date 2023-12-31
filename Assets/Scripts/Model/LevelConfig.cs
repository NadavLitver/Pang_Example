using UnityEngine;
namespace model
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Pang/Level Config", order = 1)]
    public class LevelConfig : ScriptableObject,ILevelConfig//SO that holds Level data
    {
        [SerializeField] private int levelIndex;
        [SerializeField] private bool upgradePanel;
        [SerializeField] BallData[] ballsDatas;

        public int LevelIndex { get => levelIndex;  }
        public bool UpgradePanel { get => upgradePanel; }
        public IBallData[] BallsDatas { get => ballsDatas; }
    }
}