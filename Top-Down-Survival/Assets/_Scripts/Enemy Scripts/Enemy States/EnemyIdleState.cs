using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private EnemyBase _enemy;

    public override void EnterState(EnemyBase enemy)
    {
        _enemy = enemy;
    }

    public override void UpdateState()
    {
        if (_enemy.TargetInDistance)
        {
            _enemy.SwitchToAttack();
        }
    }

    public override void UpdatePhysics()
    {
        
    }

    public override void ExitState()
    {
        
    }
}
