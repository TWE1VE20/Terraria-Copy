using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void TakeHit(int damage) {}
    public void TakeHit(int damage, GameObject attacker) {}
}
