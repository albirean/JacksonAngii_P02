using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStealPlayEffect", menuName = "CardData/PlayEffects/Steal")]

public class StealPlayEffect : CardEffect
{
    [SerializeField] int _damageAmount = 1;
    [SerializeField] int _healAmount = 1;

    public override void Activate(ITargetable target)
    {
        //test to see if the target is damageable
        IDamageable objectToDamage = target as IDamageable;
        //if it is, apply damage
        if (objectToDamage != null)
        {
            objectToDamage.LosePoints(_damageAmount);
            objectToDamage.GainPoints(_healAmount);
            Debug.Log("Add damage to the target.");
        }
        else
        {
            Debug.Log("Target is not damageable...");
        }
    }
}