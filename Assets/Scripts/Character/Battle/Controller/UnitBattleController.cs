using System.Collections.Generic;
using Combat;
using Combat.Commands;
using Battle.StateMachine;
using Character.Base;
using Character.Battle.Model;
using Character.Battle.States;
using Character.Battle.View;
using Core.Target;
using Events;
using UnityEngine;
using CharacterController = Character.Base.CharacterController;

namespace Character.Battle.Controller
{
    public class UnitBattleController : CharacterController, ITarget
    {
        public UnitBaseState CurrentState { get; private set; }
        public bool IsDead => Model.IsDead;
        public AttackCommandBase AttackCommand { get; private set; }
        public Vector3 Position => UnitView.GetAttackPosition();
        public UnitBattleModel Model { get; }
        
        protected BattleUnitView UnitView;
        private IBattleStateMachine BattleStateMachine { get; }
        private bool _isAttacking;
        private List<ITarget> _targets;

        // Cached states
        protected UnitTurnStartedState TurnStartedState;
        protected UnitTurnEndedState TurnEndedState;
        protected UnitAttackingState AttackingState;
        protected UnitTakeDamageState TakeDamageState;
        protected UnitDiedState DiedState;

        protected UnitBattleController(UnitView view, UnitBattleModel model, IBattleStateMachine battleStateMachine) : base(view, model)
        {
            Model = model;
            BattleStateMachine = battleStateMachine;
            AssignView(view);

            CharacterAnimationController animController = ((BattleUnitView)view).AnimationController;
            InitializeUnitStates(animController);

            CurrentState = TurnEndedState;
            CurrentState.EnterState();
        }

        private void InitializeUnitStates(CharacterAnimationController animController)
        {
            TurnStartedState = new UnitTurnStartedState(this, animController);
            TurnEndedState = new UnitTurnEndedState(this, animController);
            AttackingState = new UnitAttackingState(this, animController);
            TakeDamageState = new UnitTakeDamageState(this, animController);
            DiedState = new UnitDiedState(this, animController);
        }

        protected override void AssignView(UnitView view)
        {
            UnitView = (BattleUnitView)view;
            UnitView.SetController(this);
            UnitView.SetSprite(Model.Attributes.Sprite);
            UnitView.ConnectHpBar(Model.Hp);
        }

        protected void SwitchState(UnitBaseState newState)
        {
            CurrentState.ExitState();
            CurrentState = newState;
            CurrentState.EnterState();
        }

        public void SetTargets(List<ITarget> targets)
        {
            _targets = targets;
        }
        
        private void CreateAttackCommand()
        {
            AttackCommand = AttackCommandFactory.Create(
                UnitView, 
                Model.Attributes.AttackType, 
                _targets,
                Model.AttackPower,
                onStarted: OnAttackStarted,
                onEnded: OnAttackEnded
            );
        }

        public override void Destroy()
        {
            UnitView.Destroy();
        }

        public void SetInteractable(bool isUnitsTurn)
        {
            Model.IsUnitsTurn = isUnitsTurn;
        }
        
        public virtual void SetTurnStatus(bool isUnitsTurn)
        {
            if (!_isAttacking)
                SwitchState(isUnitsTurn ? TurnStartedState : TurnEndedState);
        }

        public void Attack()
        {
            if (Model.IsUnitsTurn && !Model.IsDead)
            {
                _isAttacking = true;
                CreateAttackCommand();
                SwitchState(AttackingState);
            }
        }

        public void TakeDamage(int damage)
        {
            var newHp = Model.Hp.GetHp() - damage;
            Model.Hp.HealthChange(newHp);
            
            EventBus.Publish(EventNames.DamageReceived, new DamageReceivedEvent(damage, Position + Vector3.up * 3));

            SwitchState(IsDead ? DiedState : TakeDamageState);
        }

        private void OnAttackStarted()
        {
            BattleStateMachine.TurnActionStarted();
        }

        private void OnAttackEnded()
        {
            _isAttacking = false;
            BattleStateMachine.TurnEnded();
        }
    }
}
