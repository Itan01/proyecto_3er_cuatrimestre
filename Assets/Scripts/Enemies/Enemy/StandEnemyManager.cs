using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.ProBuilder.MeshOperations;

public class StandEnemyManager : AbstractEnemy
{
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        GameMode();
    }
    protected override void FixedUpdate()
    {
        Move();
    }

}
