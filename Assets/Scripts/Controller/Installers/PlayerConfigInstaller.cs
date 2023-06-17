using model;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PlayerConfigInstaller", menuName = "Installers/PlayerConfigInstaller",order = 26)]
public class PlayerConfigInstaller : ScriptableObjectInstaller<PlayerConfigInstaller>
{
    [SerializeField] PlayerConfig playerData;
    public override void InstallBindings()
    {
        Container.Bind<PlayerConfig>().FromInstance(playerData);
    }
}