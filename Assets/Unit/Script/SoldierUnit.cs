using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoldierUnit : UnitBase
{
    protected override void Awake()
    {
        base.Awake(); // UnitBase ‚Ì Awake ‚ğŒÄ‚Ño‚·

        maxHealth = 150f;
        currentHealth = maxHealth;

        attackPower = 20f;
    }
}
