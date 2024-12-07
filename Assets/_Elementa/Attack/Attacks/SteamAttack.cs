using _Elementa.Attack.DamageVariants;
using _Elementa.Attack.Data;
using UnityEngine;

namespace _Elementa.Attack
{
    public class SteamAttack: IAttack
    {
        private AttackData _attackData;
        private SteamCloud _steamCloud;
        
        public SteamAttack(AttackData data, SteamCloud steamCloud)
        {
            _attackData = data;
            _steamCloud = steamCloud;
        }
        public void ExecuteAttack(Transform owner)
        {
            _steamCloud.gameObject.SetActive(true);
            _steamCloud.Initialize(_attackData.Damage, _attackData.LifeTime);
        }
    }
}