using UnityEngine;
using view;
using Zenject;
namespace controller
{
    [CreateAssetMenu(fileName = "SoundManagerInjector", menuName = "Installers/SoundManagerInjector", order = 8)]
    public class SoundManagerInjector : ScriptableObjectInstaller<SoundManagerInjector>
    {
        public override void InstallBindings()
        {
            Container.Bind<ISoundManager>().To<SoundManager>().FromComponentInHierarchy().AsSingle();

        }
    }
}