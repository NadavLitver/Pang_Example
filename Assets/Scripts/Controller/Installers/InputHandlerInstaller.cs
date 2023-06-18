using UnityEngine;
using Zenject;
namespace controller
{
    [CreateAssetMenu(fileName = "InputHandlerInstaller", menuName = "Installers/InputHandlerInstaller", order = 6)]

    public class InputHandlerInstaller : ScriptableObjectInstaller<InputHandlerInstaller>
    {
        [SerializeField] private InputHandler inputHandlerPrefab;
        /// <summary>
        /// create(from prefab) and install the Input Handler, Its non lazy because its refrenced using events so I needed to create an instance on the application startup
        /// </summary>
        public override void InstallBindings()
        {


            Container.Bind<IInputHandler>().To<InputHandler>().FromComponentInNewPrefab(inputHandlerPrefab).AsSingle().NonLazy();
        }
    }
}