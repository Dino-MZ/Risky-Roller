using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public EnemyBaseState CurrentState;

    public Rigidbody2D Rb;
    public EnemySO EnemySO;
    public SpriteRenderer EnemySprite;
    public Animator Anim;

    public bool TargetInDistance;
    public bool InAttackDistance;
    public bool IsShooting;

    public Transform Target;
    public Transform GroundCheck;
    public Transform EnemyObj;
    public Transform AttackPosition;
    public Transform ShootPoint;
    public Transform WallDetectionPos;
    public Transform EdgeDetectionPos;

    private void Start()
    {
        CurrentState.EnterState(this);
    }


    private void Update()
    {
        if (EnemySO.UseOverlapShere)
        {
            TargetInDistance = Physics2D.OverlapCircle(transform.position, EnemySO.ActivateDistance, EnemySO.PlayerLayer);
            InAttackDistance = Physics2D.OverlapCircle(transform.position, EnemySO.AttackDistance, EnemySO.PlayerLayer);
        }
        else
        {
            TargetInDistance = Physics2D.OverlapBox(transform.position, EnemySO.ActivateVector, 0, EnemySO.PlayerLayer);
            InAttackDistance = Physics2D.OverlapBox(transform.position, EnemySO.AttackVector, 0, EnemySO.PlayerLayer);
        }

        CurrentState.UpdateState();
    }

    private void FixedUpdate()
    {
        CurrentState.UpdatePhysics();
    }


    public abstract void SwitchToPatrol();

    public abstract void SwitchToAttack();


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(GroundCheck.position, EnemySO.GroundCheckSize);

        Gizmos.color = Color.blue;
        if (EnemySO.UseOverlapShere)
        {
            Gizmos.DrawWireSphere(transform.position, EnemySO.ActivateDistance);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(AttackPosition.position, EnemySO.AttackDistance);
        }
        else
        {
            Gizmos.DrawWireCube(transform.position, EnemySO.ActivateVector);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(AttackPosition.position, EnemySO.AttackVector);
        }

        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(WallDetectionPos.position, EnemySO.PatrolDetectionCheckSize);
        Gizmos.DrawWireSphere(EdgeDetectionPos.position, EnemySO.PatrolDetectionCheckSize);
    }

    public abstract void Shoot();


}
