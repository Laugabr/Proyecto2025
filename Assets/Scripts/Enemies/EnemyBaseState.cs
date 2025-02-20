using UnityEngine;

public  abstract class EnemyBaseState
{
    
    [SerializeField] Transform player;

    [SerializeField] LayerMask whatIsGround, whatIsPlayer;
   public  abstract void EnterState(EnemyStateManage enemy);
   public abstract void UpdateState(EnemyStateManage enemy);
   public abstract void OnCollisionEnter(EnemyStateManage enemy);
}
