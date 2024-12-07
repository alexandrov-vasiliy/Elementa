using _Elementa.Attack.DamageVariants;
using _Elementa.Attack.Data;
using _Elementa.Attack.Projectiles;
using _Elementa.Elements;
using _Elementa.ObjectPool;
using UnityEngine;
using Zenject;

namespace _Elementa.Attack
{
    public class AttackFactory: IAttackFactory
    {
       private readonly SteamCloud _steamCloud;
       private readonly AttackConfig _attackConfig;
       private readonly FindEnemy _findEnemy;
       private readonly Rock _rock;
       private readonly DiContainer _container;

       public AttackFactory(SteamCloud steamCloud, AttackConfig attackConfig, FindEnemy findEnemy, Rock rock, DiContainer container)
       {
           _steamCloud = steamCloud;
           _attackConfig = attackConfig;
           _findEnemy = findEnemy;
           _rock = rock;
           _container = container;
       }
       

       public IAttack CreateAttack(ElementData elementData, ObjectPool<Projectile> pool)
        {
            if (elementData == null || elementData.AttackData == null)
                return null;

            switch (elementData.ElementName)
            {
                case EElements.Air:
                    return  new AirAttack((ProjectileAttackData)elementData.AttackData, pool);
                case EElements.Fire:
                    return new FireAttack((ProjectileAttackData)elementData.AttackData, pool);
                case EElements.Water:
                    return new WaterAttack((ProjectileAttackData)elementData.AttackData, pool);
                case EElements.Steam:
                    return new SteamAttack(elementData.AttackData, _steamCloud);
                case EElements.Earth:
                    return new EarthAttack(_findEnemy, _attackConfig, _rock, _container);
                default:
                    Debug.LogWarning($"Неизвестный элемент: {elementData.ElementName}");
                    return  new AirAttack((ProjectileAttackData)elementData.AttackData, pool);
            }
        }
    }
}