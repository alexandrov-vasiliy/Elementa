using UnityEngine;
using Zenject;

public class GamePlayInstaller : MonoInstaller
{
    [SerializeField] private PlayerBase _player;

    public override void InstallBindings()
    {
        Container.Bind<PlayerBase>().FromInstance(_player).AsSingle();
    }
}