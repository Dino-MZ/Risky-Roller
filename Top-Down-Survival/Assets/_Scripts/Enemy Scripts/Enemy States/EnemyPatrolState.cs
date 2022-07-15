using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    private EnemyBase _enemy;

    const string LEFT = "left";
    const string RIGHT = "right";
    string _facingDirection = RIGHT;

    private bool _isTouchingWall;
    private bool _isTouchingGround;
    private float _moveSpeed;

    public override void EnterState(EnemyBase enemy)
    {
        _enemy = enemy;
        
        if(_enemy.EnemyObj.eulerAngles == Vector3.zero)
        {
            _facingDirection = RIGHT;
            ChangeFacingDirection(RIGHT);
        }
        else
        {
            _facingDirection = LEFT;
            ChangeFacingDirection(LEFT);
        }
    }

    public override void UpdateState()
    {
        _isTouchingGround = Physics2D.OverlapCircle(_enemy.EdgeDetectionPos.position, _enemy.EnemySO.PatrolDetectionCheckSize, _enemy.EnemySO.PatrolLayer);

        _isTouchingWall = Physics2D.OverlapCircle(_enemy.WallDetectionPos.position, _enemy.EnemySO.PatrolDetectionCheckSize, _enemy.EnemySO.PatrolLayer);

        if (!_isTouchingGround || _isTouchingWall)
        {
            if (_facingDirection == LEFT)
            {
                ChangeFacingDirection(RIGHT);
            }
            else
            {
                ChangeFacingDirection(LEFT);
            }
        }

        if (_enemy.TargetInDistance)
        {
            _enemy.SwitchToAttack();
        }
    }

    public override void UpdatePhysics()
    {
        _moveSpeed = _enemy.EnemySO.Speed;

        if (_facingDirection == LEFT)
        {
            _moveSpeed = -_enemy.EnemySO.Speed;
        }
        if (!_enemy.TargetInDistance)
        {
            _enemy.Rb.velocity = new Vector2(_moveSpeed * Time.fixedDeltaTime * 50, _enemy.Rb.velocity.y);
        }
    }

    public override void ExitState()
    {

    }

    void ChangeFacingDirection(string newDirection)
    {
        Vector3 direction = Vector3.zero;

        if (newDirection == LEFT)
        {
            direction = new Vector3(0, -180, 0);
        }

        _enemy.EnemyObj.eulerAngles = direction;

        _facingDirection = newDirection;

        Debug.Log("Changed Direction to " + newDirection);
    }
}
