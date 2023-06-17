using view;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SoundConfigInstaller", menuName = "Installers/SoundConfigInstaller",order = 28)]
public class SoundConfigInstaller : ScriptableObjectInstaller<SoundConfigInstaller>
{
    [SerializeField] SoundManagerConfig soundData;
    public override void InstallBindings()
    {
        Container.Bind<SoundManagerConfig>().FromInstance(soundData);
    }
}