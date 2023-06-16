using UnityEngine;
using Zenject;
namespace controller
{
    [CreateAssetMenu(fileName = "InputHandlerInjector", menuName = "Installers/InputHandlerInjector", order = 6)]

    public class InputHandlerInjector : ScriptableObjectInstaller<InputHandlerInjector>
    {
        public override void InstallBindings()
        {
            Container.Bind<IInputHandler>().To<InputHandler>().FromComponentInHierarchy().AsSingle();
        }
    }
}