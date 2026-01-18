using System.Collections.Generic;
using Character.Battle.Controller;

namespace Battle.StateMachine
{
    public interface IBattleStateMachine
    {
        public void InitBattle(List<UnitBattleController> playerControllers, List<UnitBattleController> enemyControllers);
        public void TurnActionStarted();
        public void TurnEnded();
    }
}