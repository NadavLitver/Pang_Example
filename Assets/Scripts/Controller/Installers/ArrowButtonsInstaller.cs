using System.ComponentModel;
using UnityEngine;
using view;
using Zenject;

public class ArrowButtonsInstaller : MonoInstaller
{
    [SerializeField] private ArrowButton arrowButton1;
    [SerializeField] private ArrowButton arrowButton2;

    public override void InstallBindings()
    {
        // install the corresponding Game Object based on ID
        Container.Bind<IArrowButton>().WithId("Left").To<ArrowButton>().FromInstance(arrowButton1);
        Container.Bind<IArrowButton>().WithId("Right").To<ArrowButton>().FromInstance(arrowButton2);
    }
}