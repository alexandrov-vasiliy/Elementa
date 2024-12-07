using _Elementa.Attack.Projectiles;
using _Elementa.Elements;
using _Elementa.ObjectPool;

namespace _Elementa.Attack
{
    public interface IAttackFactory
    {
        IAttack CreateAttack(ElementData elementData, ObjectPool<Projectile> pool);

    }
}