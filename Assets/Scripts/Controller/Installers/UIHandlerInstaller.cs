using UnityEngine;
using view;
using Zenject;
namespace controller
{
    [CreateAssetMenu(fileName = "UIHandlerInstaller", menuName = "Installers/UIHandlerInstaller", order = 2)]

    public class UIHandlerInstaller : ScriptableObjectInstaller<UIHandlerInstaller> 
    {
        public override void InstallBindings()
        {
            Container.Bind<IUIHandler>().To<UIHandler>().FromComponentInHierarchy().AsSingle();
        }
    }
}