using controller;
using UnityEngine;
using Zenject;

public class LevelManagerInjector : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ILevelManager>().To<LevelManager>().FromComponentInHierarchy().AsSingle().NonLazy();

    }
}