using controller;
using Zenject;

public class LocomotionInjector : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ILocomotion>().To<LocoMotion>().FromComponentInHierarchy().AsSingle();

    }
}