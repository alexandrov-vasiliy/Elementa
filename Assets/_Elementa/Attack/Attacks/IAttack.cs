using UnityEngine;

namespace _Elementa.Attack
{
    public interface IAttack
    {
        void ExecuteAttack(Transform owner);
    }
}