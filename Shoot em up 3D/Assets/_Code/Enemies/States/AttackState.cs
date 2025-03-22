using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyState
{
    public AttackState(Enemy controller) : base(controller) { }
    public override void OnEnter()
    {
        Controller.EnteringAttackState();
    }
    public override void Update()
    {
        Controller.UpdatingAttackState();
    }
    public override void OnExit()
    {
        Controller.ExitingAttackState();
    }
}
