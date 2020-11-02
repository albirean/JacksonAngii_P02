using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDividePlayEffect", menuName = "CardData/PlayEffects/Divide")]

public class DividePlayEffect : CardEffect
{
    [SerializeField] int _damageAmount = 1;

    public override void Activate(ITargetable target)
    {
        //test to see if the target is damageable
        IDamageable objectToDamage = target as IDamageable;
        //if it is, apply damage
        if (objectToDamage != null)
        {
            objectToDamage.DividePoints(_damageAmount);
            Debug.Log("Add damage to the target.");
        }
        else
        {
            Debug.Log("Target is not damageable...");
        }
    }
}
