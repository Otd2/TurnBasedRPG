using Character;
using DefaultNamespace.Target;

namespace DefaultNamespace.Attack
{
    public interface IAttack
    {
        public void Execute(int damage);
        public void ApplyDamage(int damage);
    }
}