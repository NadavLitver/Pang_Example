using controller;
using UnityEngine;
using Zenject;

public class ShootControllerInjector : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IShootController>().To<ShootController>().FromComponentInHierarchy().AsSingle();

    }
}