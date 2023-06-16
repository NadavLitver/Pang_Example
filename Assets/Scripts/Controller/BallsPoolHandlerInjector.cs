using model;
using Zenject;

public class BallsPoolHandlerInjector : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IBallsPoolHandler>().To<BallsPoolHandler>().AsSingle();
    }
}
