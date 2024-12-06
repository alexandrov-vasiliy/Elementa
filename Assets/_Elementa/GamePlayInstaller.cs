using _Elementa;
using _Elementa.Attack;
using _Elementa.Attack.DamageVariants;
using _Elementa.Elements;
using UnityEngine;
using Zenject;

public class GamePlayInstaller : MonoInstaller
{
    [SerializeField] private PlayerBase _player;
    [SerializeField] private ElementBar _elementBar;
    [SerializeField] private PassiveSkills _passiveSkills;
    [SerializeField] private SteamCloud _steamCloud;
    [SerializeField] private Rock _rock;
    [SerializeField] private AttackConfig _attackConfig;

    public override void InstallBindings()
    {
        Container.Bind<PlayerBase>().FromInstance(_player).AsSingle();
        Container.Bind<ElementBar>().FromInstance(_elementBar).AsSingle();
        Container.Bind<PassiveSkills>().FromInstance(_passiveSkills).AsSingle();
        
        Container.Bind<SteamCloud>().FromInstance(_steamCloud).AsSingle();
        Container.Bind<Rock>().FromInstance(_rock).AsSingle();
        
        Container.Bind<AttackConfig>().FromInstance(_attackConfig).AsSingle();

        Container.Bind<IAttackFactory>().To<AttackFactory>().AsSingle();

        Container.Bind<FindEnemy>().AsSingle().WithArguments(_attackConfig.EnemyMask);
    }
}