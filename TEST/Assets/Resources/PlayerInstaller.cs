using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    public Player player;
    public Transform playerSpawnPoint;
    public override void InstallBindings()
    {
        Player pl = Container.InstantiatePrefabForComponent<Player>(player, playerSpawnPoint);
        Container.Bind<Player>().FromInstance(pl).AsSingle().NonLazy();


    }
}