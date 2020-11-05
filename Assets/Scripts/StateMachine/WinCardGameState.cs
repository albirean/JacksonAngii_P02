using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCardGameState : CardGameState
{
    [SerializeField] Button _restartButton = null;
    [SerializeField] Button _quitButton = null;
    [SerializeField] Text _winText = null;
    public override void Enter()
    {
        _winText.gameObject.SetActive(true);
        _restartButton.gameObject.SetActive(true);
        _quitButton.gameObject.SetActive(true);
        Debug.Log("Entering Win State...");
    }

    public override void Exit()
    {
        _winText.gameObject.SetActive(false);
        _restartButton.gameObject.SetActive(false);
        _quitButton.gameObject.SetActive(false);
        Debug.Log("Exiting Win State");
    }
}
