using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootState : EnemyBaseState
{
    private EnemyBase _enemy;
    private bool _playerIsInFront;

    public override void EnterState(EnemyBase enemy)
    {
        _enemy = enemy;
    }

    public override void UpdateState()
    {

        _enemy.IsShooting = true;

        _playerIsInFront = _enemy.Target.transform.position.x > _enemy.transform.position.x;

        if (_playerIsInFront)
        {
            _enemy.EnemyObj.eulerAngles = Vector3.zero;
        }
        else
        {
            _enemy.EnemyObj.eulerAngles = new Vector3(0, -180, 0);
        }

        _enemy.Anim.SetBool("IsShooting", _enemy.IsShooting);

        if (!_enemy.InAttackDistance)
        {
            _enemy.SwitchToPatrol();
        }
    }

    public override void UpdatePhysics()
    {
        
    }
    public override void ExitState()
    {
        _enemy.IsShooting = false;
        _enemy.Anim.SetBool("IsShooting", _enemy.IsShooting);
    }
}
