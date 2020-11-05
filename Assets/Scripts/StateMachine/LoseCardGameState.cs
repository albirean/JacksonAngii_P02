using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseCardGameState : CardGameState
{
    [SerializeField] Button _restartButton = null;
    [SerializeField] Button _quitButton = null;
    [SerializeField] Text _loseText = null;
    public override void Enter()
    {
        _restartButton.gameObject.SetActive(true);
        _quitButton.gameObject.SetActive(true);
        _loseText.gameObject.SetActive(true);
        Debug.Log("Entering Lose State...");
    }

    public override void Exit()
    {
        _restartButton.gameObject.SetActive(false);
        _quitButton.gameObject.SetActive(false);
        _loseText.gameObject.SetActive(false);
        Debug.Log("Exiting Lose State");
    }
}
