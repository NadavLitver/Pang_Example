using model;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PlayerConfigInstaller", menuName = "Installers/PlayerConfigInstaller",order = 26)]
public class PlayerConfigInstaller : ScriptableObjectInstaller<PlayerConfigInstaller>
{
    [SerializeField] PlayerConfig playerData;
    /// <summary>
    /// Install PlayerConfig SO from instance
    /// </summary>
    public override void InstallBindings()
    {
        Container.Bind<IPlayerConfig>().To<PlayerConfig>().FromInstance(playerData);
    }
}