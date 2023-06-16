using view;
using Zenject;

public class BlinkOnHitInjector : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IBlinkOnHit>().To<BlinkOnHit>().FromComponentInHierarchy().AsSingle();

    }
}