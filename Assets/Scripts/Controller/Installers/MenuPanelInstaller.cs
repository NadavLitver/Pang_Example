using controller;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MenuPanelInstaller : MonoInstaller
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button startButton;
    [SerializeField] private Button resetButton;
    public override void InstallBindings()
    {

        Container.Bind<GameObject>().FromInstance(pauseMenuUI).WhenInjectedInto<MenuPanel>();
        Container.Bind<Button>().WithId("PauseButton").FromInstance(pauseButton).WhenInjectedInto<MenuPanel>();
        Container.Bind<Button>().WithId("StartButton").FromInstance(startButton).WhenInjectedInto<MenuPanel>();
        Container.Bind<Button>().WithId("ResetButton").FromInstance(resetButton).WhenInjectedInto<MenuPanel>();

        Container.Bind<IMenuPanel>().To<MenuPanel>().FromNew().AsSingle().NonLazy();

    }
}