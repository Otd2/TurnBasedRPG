using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

public class TurnStateManager : ITurnManager
{
    private BattleBoardController.GameState _activeGameState;
    private List<CharacterBattleController> _playerControllers;
    private List<CharacterBattleController> _enemyControllers;
    
    Action<BattleBoardController.GameState> _stateChangeAction;

    public TurnStateManager(Action<BattleBoardController.GameState> OnStateChanged)
    {
        _stateChangeAction += OnStateChanged;
    }

    public void InitTurn(BattleBoardController.GameState activeGameState, 
        List<CharacterBattleController> playerControllers, List<CharacterBattleController> enemyControllers)
    {
        _activeGameState = activeGameState;
        _playerControllers = playerControllers;
        _enemyControllers = enemyControllers;
        StateChanged();
    }

    public void StateChanged()
    {
        switch (_activeGameState)
        {
            case BattleBoardController.GameState.PlayerTurn:
                _playerControllers.ForEach((player) => player.SetCharacterState(CharacterBattleState.Active));
                _enemyControllers.ForEach((enemy) => enemy.SetCharacterState(CharacterBattleState.Busy));
                break;
            case BattleBoardController.GameState.EnemyState:
                _playerControllers.ForEach((player) => player.SetCharacterState(CharacterBattleState.Busy));
                _enemyControllers.ForEach((enemy) => enemy.SetCharacterState(CharacterBattleState.Active));
                break;
            case BattleBoardController.GameState.PlayerActionInProgress:
                _playerControllers.ForEach((player) => player.SetCharacterState(CharacterBattleState.Busy));
                break;
            case BattleBoardController.GameState.EnemyActionInProgress:
                _enemyControllers.ForEach((enemy) => enemy.SetCharacterState(CharacterBattleState.Busy));
                break;
            case BattleBoardController.GameState.Win:
                Debug.Log("WIN");
                break;
            case BattleBoardController.GameState.Lose:
                Debug.Log("LOSE");
                break;
            default:
                throw new ArgumentOutOfRangeException();
            
        }

        _stateChangeAction?.Invoke(_activeGameState);
    }
    public void TurnActionStarted()
    {
        if (_activeGameState == BattleBoardController.GameState.PlayerTurn)
            _activeGameState = BattleBoardController.GameState.PlayerActionInProgress;
        if (_activeGameState == BattleBoardController.GameState.EnemyState)
            _activeGameState = BattleBoardController.GameState.EnemyActionInProgress;
         
        StateChanged();
    }

    public void TurnEnded()
    {
        if (CheckIfGameOver())
        {
            StateChanged();
            return;
        }
        
        switch (_activeGameState)
        {
            case BattleBoardController.GameState.PlayerActionInProgress:
                _activeGameState = BattleBoardController.GameState.EnemyState;
                StateChanged();
                break;
            case BattleBoardController.GameState.EnemyActionInProgress:
                _activeGameState = BattleBoardController.GameState.PlayerTurn;
                StateChanged();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    
    private bool CheckIfGameOver()
    {
        if (CheckIfAllCharacterDead(_enemyControllers))
        {
            _activeGameState = BattleBoardController.GameState.Win;
            return true;
        }
        
        if (CheckIfAllCharacterDead(_playerControllers))
        {
            _activeGameState = BattleBoardController.GameState.Lose;
            return true;
        }

        return false;
    }


    private bool CheckIfAllCharacterDead(List<CharacterBattleController> battleCharacters)
    {
        foreach (var battleController in battleCharacters)
        {
            if (!battleController.IsDead)
                return false;
        }

        return true;
    }

}

public interface ITurnManager
{
    public void InitTurn(BattleBoardController.GameState activeGameState, List<CharacterBattleController> playerControllers, List<CharacterBattleController> enemyControllers);
    public void StateChanged();
    public void TurnActionStarted();
    public void TurnEnded();
}
