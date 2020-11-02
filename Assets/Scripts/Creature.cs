using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour, ITargetable, IDamageable
{
    public int _currentPoints = 0;
    public int _maxPoints = 30;


    public void LosePoints(int damage)
    {
        _currentPoints -= damage;
        Debug.Log("Took damage. Remaining health: " + _currentPoints);
    }

    public void GainPoints(int health)
    {
        _currentPoints += health;
        Debug.Log("Healed. Remaining Health: " + _currentPoints);
        if(_currentPoints >= _maxPoints)
        {
            _currentPoints = _maxPoints;
        }
    }

    public void DividePoints(int divide)
    {
        _currentPoints *= (1 / divide);
        Debug.Log("Divided. Remaining Health: " + _currentPoints);
    }

    public void MultiplyPoints(int multiply)
    {
        _currentPoints *= multiply;
        Debug.Log("Multiplied. Remaining Health: " + _currentPoints);
        if(_currentPoints >= _maxPoints)
        {
            _currentPoints = _maxPoints;
        }
    }
    public void Target()
    {
        Debug.Log(gameObject.name + " has been targeted.");
    }
}
