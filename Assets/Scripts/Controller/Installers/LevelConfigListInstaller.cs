using model;
using UnityEngine;
using Zenject;
namespace controller
{
    [CreateAssetMenu(fileName = "LevelConfigListInstaller", menuName = "Installers/LevelConfigListInstaller",order = 35)]
    public class LevelConfigListInstaller : ScriptableObjectInstaller<LevelConfigListInstaller>
    {
        [SerializeField] LevelConfigList LevelListData;
        public override void InstallBindings()
        {
            Container.Bind<LevelConfigList>().FromInstance(LevelListData);

        }
    }
}