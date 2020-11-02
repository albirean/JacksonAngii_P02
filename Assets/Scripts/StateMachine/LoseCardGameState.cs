using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseCardGameState : CardGameState
{
    public override void Enter()
    {
        Debug.Log("Entering Lose State...");
    }

    public override void Exit()
    {
        Debug.Log("Exiting Lose State");
    }
}
