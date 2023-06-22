using UnityEngine;
using Zenject;
using view;
namespace controller
{
    [CreateAssetMenu(fileName = "InputHandlerInstaller", menuName = "Installers/InputHandlerInstaller", order = 6)]

    public class InputHandlerInstaller : ScriptableObjectInstaller<InputHandlerInstaller>
    {
        /// <summary>
        /// create and install the Input Handler, Its non lazy because I needed to create an instance on the application startup
        /// </summary>
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<InputHandler>().FromNew().AsSingle().NonLazy();
        }
    }
}