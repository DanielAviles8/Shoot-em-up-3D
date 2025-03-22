using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingState : EnemyState
{
    public ChasingState(Enemy controller) : base(controller) { }

    public override void OnEnter()
    {
        Controller.EnteringChasingState();
    }
    public override void Update()
    {
        Controller.UpdatingChasingState();
    }
    public override void OnExit() 
    {
        
    }
}
