using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour, ITargetable, IDamageable
{
    int _currentHealth = 10;
    int _maxHealth = 25;

    public void Kill()
    {
        Debug.Log("Kill the creature!");
        gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        Debug.Log("Took damage. Remaining health: " + _currentHealth);
        if(_currentHealth <= 0)
        {
            Kill();
        }
    }

    public void HealDamage(int health)
    {
        _currentHealth += health;
        Debug.Log("Healed. Remaining Health: " + _currentHealth);
        if(_currentHealth >= _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }
    public void Target()
    {
        Debug.Log("Creature has been targeted.");
    }
}
