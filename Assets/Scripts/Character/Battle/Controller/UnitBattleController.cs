using System.Collections.Generic;
using Attack;
using BattleStates.StateMachine;
using Character.Base;
using Character.Battle.Model;
using Character.Battle.States;
using Character.Battle.View;
using DefaultNamespace.Target;
using Events;
using UnityEngine;
using CharacterController = Character.Base.CharacterController;

namespace Character.Battle.Controller
{
    public class UnitBattleController : CharacterController, ITarget
    { 
        
        public UnitBaseState CurrentState;
        public bool IsDead => Model.IsDead;
        public AttackCommandBase AttackCommand { get; private set; }
        public Vector3 Position => UnitView.GetAttackPosition();
        public UnitBattleModel Model { get; }
        
        protected BattleUnitView UnitView;
        protected readonly UnitStateFactory Factory;
        private IBattleStateMachine BattleStateMachine { get; }
        private bool _isAttacking;

        protected UnitBattleController(UnitView view, UnitBattleModel model, IBattleStateMachine battleStateMachine) : base(view, model)
        {
            Model = model;
            BattleStateMachine = battleStateMachine;
            AssignView(view);
            
            //State factory to create Unit States
            Factory = new UnitStateFactory(this, ((BattleUnitView)view).AnimationController);
            
            //Start with inactive state
            CurrentState = Factory.TurnEndedState;
            CurrentState.EnterState();
        }
        
        protected override void AssignView(UnitView view)
        {
            UnitView = (BattleUnitView)view;
            UnitView.SetController(this);
            UnitView.SetSprite(Model.Attributes.Sprite);
            UnitView.ConnectHpBar(Model.Hp);
        }
        
        private List<ITarget> _targets;
        
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

        //called by battle state machine
        public void SetInteractable(bool isUnitsTurn)
        {
            Model.IsUnitsTurn = isUnitsTurn;
        }
        
        public virtual void SetTurnStatus(bool isUnitsTurn)
        {
            if(!_isAttacking)
                CurrentState.SwitchState(isUnitsTurn ? Factory.TurnStartedState : Factory.TurnEndedState);
        }


        public void Attack()
        {
            if (Model.IsUnitsTurn && !Model.IsDead)
            {
                _isAttacking = true;
                CreateAttackCommand();
                CurrentState.SwitchState(Factory.AttackingSate);
            }
        }

        #region Target Listener
        public void TakeDamage(int damage)
        {
            CurrentState.SwitchState(Factory.CreateTakeDamageState(damage));
            EventBus.Publish(EventNames.DamageReceived, new DamageReceivedEvent(damage, Position + Vector3.up * 3));
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
        #endregion
    }
}