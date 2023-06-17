using UnityEngine;
using Zenject;
namespace controller
{
    [CreateAssetMenu(fileName = "RobotAnimatorInstaller", menuName = "Installers/RobotAnimatorInstaller", order = 10)]
    public class RobotAnimatorInstaller : ScriptableObjectInstaller<RobotAnimatorInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IRobotAnimatorUpdater>().To<RobotAnimatorUpdater>().FromComponentInHierarchy().AsSingle();

        }
    }
}