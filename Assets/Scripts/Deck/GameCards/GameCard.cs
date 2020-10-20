using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCard : Card
{
    public int Cost { get; private set; }
    public Sprite Graphic { get; private set; }
    public CardEffect PlayEffect { get; private set; }
    public GameCard(GameCardData Data)
    {
        Name = Data.Name;
        Cost = Data.Cost;
        Graphic = Data.Graphic;
        PlayEffect = Data.PlayEffect;
    }

    public override void Play()
    {
        ITargetable target = TargetController.CurrentTarget;
        Debug.Log("Playing ability card: " + Name + " on target.");
        PlayEffect.Activate(target);
    }
}
