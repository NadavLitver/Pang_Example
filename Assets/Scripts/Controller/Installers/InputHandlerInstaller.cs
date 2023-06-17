using UnityEngine;
using Zenject;
namespace controller
{
    [CreateAssetMenu(fileName = "InputHandlerInstaller", menuName = "Installers/InputHandlerInstaller", order = 6)]

    public class InputHandlerInstaller : ScriptableObjectInstaller<InputHandlerInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IInputHandler>().To<InputHandler>().FromComponentInHierarchy().AsSingle();
        }
    }
}