using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewHealPlayEffect", menuName = "CardData/PlayEffects/Heal")]
public class HealPlayEffect : CardEffect
{
    [SerializeField] int _healAmount = 1;

    public override void Activate(ITargetable target)
    {
        //test to see if the target is damageable
        IDamageable objectToDamage = target as IDamageable;
        //if it is, apply damage
        if (objectToDamage != null)
        {
            objectToDamage.HealDamage(_healAmount);
            Debug.Log("Add Health to the target.");
        }
        else
        {
            Debug.Log("Target is not damageable...");
        }
    }
}
