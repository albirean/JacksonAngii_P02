using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGameUIController : MonoBehaviour
{
    [SerializeField] Text _enemyThinkingTextUI = null;
    [SerializeField] Slider _playerPoints = null;
    [SerializeField] Slider _enemyPoints = null;
    [SerializeField] Text _playerPointText = null;
    [SerializeField] Text _enemyPointText = null;

    [SerializeField] Creature _player = null;
    [SerializeField] Creature _enemy = null;

    private void OnEnable()
    {
        EnemyTurnCardGameState.EnemyTurnBegan += OnEnemyTurnBegan;
        EnemyTurnCardGameState.EnemyTurnEnded += OnEnemyTurnEnded;
    }

    private void OnDisable()
    {
        EnemyTurnCardGameState.EnemyTurnBegan -= OnEnemyTurnBegan;
        EnemyTurnCardGameState.EnemyTurnEnded -= OnEnemyTurnEnded;
    }

    private void Start()
    {
        //make sure text is disabled on start
        _enemyThinkingTextUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        _playerPoints.value = _player._currentPoints;
        _playerPointText.text = _player._currentPoints.ToString();
        _enemyPoints.value = _enemy._currentPoints;
        _enemyPointText.text = _enemy._currentPoints.ToString();
    }

    void OnEnemyTurnBegan()
    {
        _enemyThinkingTextUI.gameObject.SetActive(true);
    }

    void OnEnemyTurnEnded()
    {
        _enemyThinkingTextUI.gameObject.SetActive(false);
    }
}
