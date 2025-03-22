using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingState : EnemyState
{
    public WanderingState(Enemy controller) : base(controller) { }
    public override void OnEnter()
    {
        Controller.EnteringWanderingState();
    }
    public override void Update()
    {
        Controller.UpdatingWanderingState();
    }
    public override void OnExit() 
    { 

    }
}