using UnityEngine;
using view;
using Zenject;
namespace controller
{
    [CreateAssetMenu(fileName = "SoundManagerInstaller", menuName = "Installers/SoundManagerInstaller", order = 8)]
    public class SoundManagerInstaller : ScriptableObjectInstaller<SoundManagerInstaller>
    {
        /// <summary>
        /// Create and install the soundmanager
        /// </summary>
        public override void InstallBindings()
        {
            Container.Bind<ISoundManager>().To<SoundManager>().FromNew().AsSingle();

        }
    }
}