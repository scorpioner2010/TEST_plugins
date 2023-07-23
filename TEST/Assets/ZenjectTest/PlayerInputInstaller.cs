using UnityEngine;
using Zenject;

public class PlayerInputInstaller : MonoInstaller
{
    public PlayerInput input;
    public override void InstallBindings()
    {
        Container.Bind<IPlayerInput>().To<PlayerInput>().FromComponentInNewPrefab(input).AsSingle();
    }
}