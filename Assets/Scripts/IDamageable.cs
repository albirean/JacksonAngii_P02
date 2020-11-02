using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void LosePoints(int damage);

    void GainPoints(int health);

    void MultiplyPoints(int multiply);

    void DividePoints(int divide);
}
