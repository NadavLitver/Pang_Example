using UnityEngine;
using Zenject;
namespace controller
{
    [CreateAssetMenu(fileName = "InputHandlerInstaller", menuName = "Installers/InputHandlerInstaller", order = 6)]

    public class InputHandlerInstaller : ScriptableObjectInstaller<InputHandlerInstaller>
    {
        [SerializeField] private InputHandler inputHandlerPrefab;

        public override void InstallBindings()
        {
            Container.Bind<IInputHandler>().To<InputHandler>().FromComponentInNewPrefab(inputHandlerPrefab).AsSingle().NonLazy();
        }
    }
}