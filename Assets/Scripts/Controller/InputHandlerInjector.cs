using Zenject;
namespace controller
{
    public class InputHandlerInjector : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IInputHandler>().To<InputHandler>().FromComponentInHierarchy().AsSingle();
        }
    }
}