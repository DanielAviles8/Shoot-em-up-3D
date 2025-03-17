using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyState
{
    public AttackState(Enemy controller) : base(controller) { }
    public override void OnEnter()
    {
        Debug.Log("Entering Attack State");
    }
    public override void Update()
    {

    }
    public override void OnExit()
    {

    }
}
