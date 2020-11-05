using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnCardGameState : CardGameState
{
    [SerializeField] Text _playerTurnTextUI = null;
    [SerializeField] Creature _player = null;
    [SerializeField] Creature _enemy = null;

    int _playerTurnCount = 0;

    public override void Enter()
    {
        Debug.Log("Player Turn: ...Entering");
        _playerTurnTextUI.gameObject.SetActive(true);

        _playerTurnCount++;
        _playerTurnTextUI.text = "Player Turn: " + _playerTurnCount.ToString();

        StateMachine.Input.PressedConfirm += OnPressedConfirm;
    }

    public override void Tick()
    {
        if(_player._currentPoints == 30)
        {
            Debug.Log("Entering Win State...");
            StateMachine.ChangeState<WinCardGameState>();
        }

        if(_enemy._currentPoints == 30)
        {
            Debug.Log("Entering Lose State...");
            StateMachine.ChangeState<LoseCardGameState>();
        }
    }

    public override void Exit()
    {
        _playerTurnTextUI.gameObject.SetActive(false);

        StateMachine.Input.PressedConfirm -= OnPressedConfirm;
        Debug.Log("Player Turn: Exiting...");
    }

    void OnPressedConfirm()
    {
        Debug.Log("Attempt to enter Enemy State!");
        StateMachine.ChangeState<EnemyTurnCardGameState>();
    }
}
