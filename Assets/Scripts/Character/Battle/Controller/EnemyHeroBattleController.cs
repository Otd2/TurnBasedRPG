using System;
using Cysharp.Threading.Tasks;
using DefaultNamespace;

namespace Character
{
    public class EnemyHeroBattleController : CharacterBattleController
    {
        private BattleUnitView _view;
        
        public override void SetCharacterState(CharacterBattleState battleState)
        {
            base.SetCharacterState(battleState);
            if (isActive)
                DelayedAttack();
        }

        async void DelayedAttack()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), ignoreTimeScale: false);
            Attack();
        }

        protected override void HpOnOnHealthChanged(int newHealth)
        {
            PlayerPrefsPersistentDataManager.CurrentGameData.BattleData.enemyHp = newHealth;
        }

        public EnemyHeroBattleController(UnitView view, UnitBattleModel model, PlayerPrefsPersistentDataManager playerPrefsPersistentDataManager, ITurnManager turnManager) : base(view, model, playerPrefsPersistentDataManager, turnManager)
        {
        }
    }
}