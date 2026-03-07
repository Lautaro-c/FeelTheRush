using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    public virtual void StateMachine()
    {
    }

    public virtual void ChasePlayer()
    {
    }

    public virtual void EndScreamAnimation()
    {
    }

    public virtual void AttackPlayer()
    {
    }

    public virtual void ResetAttack()
    {
        
    }

    public virtual void EnemyDied()
    {
        
    }

    public virtual void EnemyRevived(Vector3 originalPos)
    {

    }

}
