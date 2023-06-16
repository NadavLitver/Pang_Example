using UnityEngine;
using view;
using Zenject;
namespace controller
{
    [CreateAssetMenu(fileName = "BlinkOnHitInjector", menuName = "Installers/BlinkOnHitInjector", order = 21)]

    public class BlinkOnHitInjector : ScriptableObjectInstaller<BlinkOnHitInjector>
    {
        public override void InstallBindings()
        {
            Container.Bind<IBlinkOnHit>().To<BlinkOnHit>().FromComponentInHierarchy().AsSingle();

        }
    }
}