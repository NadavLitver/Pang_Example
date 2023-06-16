using UnityEngine;
using Zenject;
namespace controller
{
    [CreateAssetMenu(fileName = "LevelManagerInjector", menuName = "Installers/LevelManagerInjector", order = 4)]
    public class LevelManagerInjector : ScriptableObjectInstaller<LevelManagerInjector>
    {

        public override void InstallBindings()
        {
            Container.Bind<ILevelManager>().To<LevelManager>().FromComponentInHierarchy().AsSingle().NonLazy();

        }
    }
}