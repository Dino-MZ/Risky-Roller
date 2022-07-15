using UnityEngine;

public abstract class EnemyBaseState 
{
    public abstract void EnterState(EnemyBase enemy);

    public abstract void UpdateState();

    public abstract void UpdatePhysics();

    public abstract void ExitState();


}
