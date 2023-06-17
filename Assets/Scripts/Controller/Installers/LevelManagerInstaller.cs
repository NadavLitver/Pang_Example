using UnityEngine;
using Zenject;
namespace controller
{
    [CreateAssetMenu(fileName = "LevelManagerInstaller", menuName = "Installers/LevelManagerInstaller", order = 4)]
    public class LevelManagerInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private LevelManager levelManager;
        public override void InstallBindings()
        {
            Debug.Log("Trying to bind Level Manager");
            Container.Bind<ILevelManager>().To<LevelManager>().FromComponentInNewPrefab(levelManager).AsSingle();

        }
    }
}