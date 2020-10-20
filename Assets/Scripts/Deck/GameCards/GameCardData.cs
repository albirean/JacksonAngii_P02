using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameCard",
    menuName = "CardData/GameCard")]

public class GameCardData : ScriptableObject
{
    [SerializeField] string _name = "...";
    public string Name => _name;

    [SerializeField] int _cost = 1;
    public int Cost => _cost;

    [SerializeField] Sprite _graphic = null;
    public Sprite Graphic => _graphic;

    [SerializeField] CardEffect _playEffect = null;
    public CardEffect PlayEffect => _playEffect;
}
