using view;
using Zenject;

public class UIHandlerInjector : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IUIHandler>().To<UIHandler>().FromComponentInHierarchy().AsSingle();
    }
}