using Character.Battle.Controller;

namespace Character.Battle.States
{
    public class UnitTakeDamageState : UnitBaseState
    {
        private readonly int _damage;

        public override void EnterState()
        {
            var newHp =  UnitController.Model.Hp.GetHp() - _damage; 
            UnitController.Model.Hp.HealthChange(newHp);
            if (UnitController.IsDead)
            {
                SwitchState(Factory.DiedState);
                return;
            }
            CharacterAnimationController.PlayAnimation("Hit");
        }

        public override void ExitState()
        {
        }
        public UnitTakeDamageState(UnitBattleController unitController,
            CharacterAnimationController characterAnimationController, UnitStateFactory factory,
            int damage)
            : base(unitController, characterAnimationController, factory)
        {
            _damage = damage;
        }
    }
}