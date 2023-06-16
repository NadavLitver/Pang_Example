using Zenject;
namespace controller
{
    public class BallControllerInjector : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IBallController>().To<BallController>().FromComponentInHierarchy().AsSingle();

        }
    }
}