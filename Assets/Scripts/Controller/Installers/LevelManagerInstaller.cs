using UnityEngine;
using Zenject;
namespace controller
{
    [CreateAssetMenu(fileName = "LevelManagerInstaller", menuName = "Installers/LevelManagerInstaller", order = 4)]
    public class LevelManagerInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private LevelManager levelManager;
        /// <summary>
        /// create(from prefab) and install the Level Manager
        /// </summary>
        public override void InstallBindings()
        {

            Container.Bind<ILevelManager>().To<LevelManager>().FromComponentInNewPrefab(levelManager).AsSingle();

        }
    }
}