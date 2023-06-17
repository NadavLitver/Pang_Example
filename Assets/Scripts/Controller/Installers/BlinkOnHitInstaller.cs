using UnityEngine;
using view;
using Zenject;
namespace controller
{
    [CreateAssetMenu(fileName = "BlinkOnHitInstaller", menuName = "Installers/BlinkOnHitInstaller", order = 21)]

    public class BlinkOnHitInstaller : ScriptableObjectInstaller<BlinkOnHitInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IBlinkOnHit>().To<BlinkOnHit>().FromComponentInHierarchy().AsSingle();

        }
    }
}