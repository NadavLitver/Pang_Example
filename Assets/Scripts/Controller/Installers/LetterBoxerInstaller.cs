using UnityEngine;
using view;
using Zenject;

[CreateAssetMenu(fileName = "LetterBoxerInstaller", menuName = "Installers/LetterBoxerInstaller", order = 38)]
public class LetterBoxerInstaller : ScriptableObjectInstaller<LetterBoxerInstaller>
{
    /// <summary>
    /// create and install the letterboxer
    /// </summary>
    public override void InstallBindings()
    {
        Container.Bind<ILetterBoxer>().To<LetterBoxer>().FromNew().AsSingle().NonLazy();
    }
}