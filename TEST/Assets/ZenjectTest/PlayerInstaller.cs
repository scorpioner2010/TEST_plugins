using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    public Player player;
    public Transform playerSpawnPoint;

    public override void InstallBindings()
    {
        Player pl = Container.InstantiatePrefabForComponent<Player>(player, new Vector3(0, 0, 0), Quaternion.identity,
            null);
        Container.Bind<Player>().FromInstance(pl).AsSingle().NonLazy();


    }
}