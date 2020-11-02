using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCardGameState : CardGameState
{
    public override void Enter()
    {
        Debug.Log("Entering Win State...");
    }

    public override void Exit()
    {
        Debug.Log("Exiting Win State");
    }
}
