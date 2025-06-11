using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StandEnemyManager : AbstractEnemy
{
    protected override void FixedUpdate()
    {
        Move(); // en abstractEnemy
    }

}
