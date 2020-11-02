using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMultiplyPlayEffect", menuName = "CardData/PlayEffects/Multiply")]

public class MultiplyPlayEffect : CardEffect
{
    [SerializeField] int _multiplyAmount = 2;

    public override void Activate(ITargetable target)
    {
        //test to see if the target is damageable
        IDamageable objectToDamage = target as IDamageable;
        //if it is, apply damage
        if (objectToDamage != null)
        {
            objectToDamage.MultiplyPoints(_multiplyAmount);
            Debug.Log("Multiply target's points.");
        }
        else
        {
            Debug.Log("Target is not damageable...");
        }
    }
}
