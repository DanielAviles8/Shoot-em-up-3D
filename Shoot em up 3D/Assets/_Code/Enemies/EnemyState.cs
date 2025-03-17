using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : State
{
    protected Enemy Controller;
     
    public EnemyState(Enemy controller)
    {
        Controller = controller;
    }
}
