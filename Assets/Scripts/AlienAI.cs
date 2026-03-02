using UnityEngine;
using UnityEngine.AI;

public class AlienAI : EnemyAI
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    private bool enemyDead;
    private bool enemyJustSeen = false;

    //idle
    private Animator enemyAnimator;
    [SerializeField] private float screamTime;

    //Attacking 
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private bool alreadyAttacked;

    //States
    [SerializeField] private float sightRange;
    [SerializeField] private float attackRange;
    [SerializeField] private bool playerInSightRange;
    [SerializeField] private bool playerInAttackRange;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponentInChildren<Animator>();
        enemyDead = false;
    }

    private void Update()
    {
        StateMachine();
    }

    public override void StateMachine()
    {
        if (!enemyDead)
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange)
            {
                enemyJustSeen = false;
            }
            if (playerInSightRange && !playerInAttackRange)
            {
                ChasePlayer();
            }
            if (playerInSightRange && playerInAttackRange)
            {
                AttackPlayer();
            }
        }
    }

    public override void ChasePlayer()
    {
        transform.LookAt(player);
        if (!enemyJustSeen)
        {
            enemyAnimator.SetTrigger("PlayerSeen");
            Invoke(nameof(EndScreamAnimation), screamTime);
        }
        else
        {
            enemyAnimator.SetBool("StartChasing", true);
            agent.SetDestination(player.position);
        }
    }

    public override void EndScreamAnimation()
    {
        enemyJustSeen = true;
    }

    public override void AttackPlayer()
    {
        enemyAnimator.SetBool("StartChasing", false);
        transform.LookAt(player);
        agent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            //Attack code
            enemyAnimator.SetTrigger("AttackPlayer");
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }

    public override void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public override void EnemyDied()
    {
        enemyDead = true;
    }

    public override void EnemyRevived()
    {
        enemyDead = false;
    }
}
