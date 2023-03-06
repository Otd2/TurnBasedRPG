using System;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Attack;
using DefaultNamespace.Health;
using DefaultNamespace.Target;
using UnityEngine;

namespace Character
{
    public class CharacterBattleController : CharacterController, ITarget
    {
        public static event Action<int, Vector3> OnAnyDamageReceived = delegate(int damage, Vector3 pos) {  }; 
        public bool IsDead => _model.Hp.GetHp() <= 0;
        
        protected bool isActive;
        protected UnitBattleModel _model;
        protected ITurnManager _turnManager;
        
        protected BattleUnitView _unitView;
        private readonly List<ITarget> _target;
        private AttackBase _attackBase;

        protected CharacterBattleController(UnitView view, UnitBattleModel model, 
            PlayerPrefsPersistentDataManager playerPrefsPersistentDataManager, 
            ITurnManager turnManager) : base(view, model, playerPrefsPersistentDataManager)
        {
            _model = model;
            _turnManager = turnManager;
            AssignView(view);
        }


        public override void Destroy()
        {
            _unitView.Destroy();
        }
        protected override void AssignView(UnitView view)
        {
            _unitView = (BattleUnitView)view;
            _unitView.SetController(this);
            _unitView.SetSprite(_model.Attributes.Sprite);
            ((DynamicHealth)_model.Hp).OnHealthChanged += HpOnOnHealthChanged;
            _unitView.SetHpBar(((DynamicHealth)_model.Hp));
        }

        private void AttackBaseOnOnAttackEnd()
        {
            _turnManager.TurnEnded();
        }
        
        private void OnAttackStarted()
        {
            _turnManager.TurnActionStarted();
        }
        
        public virtual void SetCharacterState(CharacterBattleState battleState)
        {
            isActive = battleState == CharacterBattleState.Active && !IsDead;
        }

        public void SetAttackStrategy(List<ITarget> targets)
        {
            switch (_model.Attributes.SkillSet[0])
            {
                case AttackType.MeleeTargeted:
                    _attackBase = new MeleeTargetedAttack(_unitView.transform, targets[0], 0, 2f);
                    break;
                case AttackType.MeleeRandom:
                    _attackBase = new MeleeRandomAttack(_unitView.transform, targets, 0, 3f);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            _attackBase.OnAttackEnd += AttackBaseOnOnAttackEnd;
            _attackBase.OnAttackStarted += OnAttackStarted;
        }


        protected virtual void HpOnOnHealthChanged(int newHealth)
        {
            PlayerPrefsPersistentDataManager.OnCharacterHPChanged(_model.Id, newHealth);
        }

        public Vector3 Position => _unitView.GetAttackPoint();

        public void TakeDamage(int damage)
        {
            var newHp =  _model.Hp.GetHp() - damage; 
            ((DynamicHealth)_model.Hp).HealthChange(newHp);
            OnAnyDamageReceived?.Invoke(damage, Position + Vector3.up * 7);
        }

        public void Attack()
        {
            if(isActive)
                _attackBase.Execute(_model.AttackPower);
        }
        
    }
    
    public enum CharacterBattleState
    {
        Active,
        Busy
    }
}