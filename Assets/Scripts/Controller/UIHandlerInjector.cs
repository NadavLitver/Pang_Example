using UnityEngine;
using view;
using Zenject;
namespace controller
{
    [CreateAssetMenu(fileName = "UIHandlerInjector", menuName = "Installers/UIHandlerInjector", order = 2)]

    public class UIHandlerInjector : ScriptableObjectInstaller<UIHandlerInjector> 
    {
        public override void InstallBindings()
        {
            Container.Bind<IUIHandler>().To<UIHandler>().FromComponentInHierarchy().AsSingle();
        }
    }
}