using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoldierUnit : UnitBase
{
    protected override void Awake()
    {
        maxHealth = 150f;

        attackPower = 20f;
        defensePower = 5f;

        base.Awake(); // UnitBase ‚Ì Awake ‚ğŒÄ‚Ño‚·
    }
}
