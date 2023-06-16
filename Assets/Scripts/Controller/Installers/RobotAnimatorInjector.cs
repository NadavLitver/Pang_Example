using UnityEngine;
using Zenject;
namespace controller
{
    [CreateAssetMenu(fileName = "RobotAnimatorInstaller", menuName = "Installers/RobotAnimatorInjector", order = 10)]
    public class RobotAnimatorInjector : ScriptableObjectInstaller<RobotAnimatorInjector>
    {
        public override void InstallBindings()
        {
            Container.Bind<IRobotAnimatorUpdater>().To<RobotAnimatorUpdater>().FromComponentInHierarchy().AsSingle();

        }
    }
}