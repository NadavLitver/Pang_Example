using UnityEngine;
using view;
using Zenject;

public class SoundManagerInjector : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ISoundManager>().To<SoundManager>().FromComponentInHierarchy().AsSingle();

    }
}