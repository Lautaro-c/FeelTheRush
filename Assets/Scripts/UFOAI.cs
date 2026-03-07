using UnityEngine;
using UnityEngine.AI;

public class UFOAI : EnemyAI
{
    public Transform player;
    public LayerMask whatIsPlayer;
    private bool enemyDead;
    private bool enemyJustSeen = false;

    //idle
    private Animator enemyAnimator;
    [SerializeField] private float screamTime;

    //Attacking 
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private bool alreadyAttacked;

    //Chasing
    [SerializeField] private float moveSpeed;
    [SerializeField] private float obstacleAvoidanceRange;
    [SerializeField] private float heightOffset;
    [SerializeField] private float heightAdjustSpeed;

    //States
    [SerializeField] private float sightRange;
    [SerializeField] private float attackRange;
    [SerializeField] private bool playerInSightRange;
    [SerializeField] private bool playerInAttackRange;

    //Projectile
    [SerializeField] private float projectileForce;
    [SerializeField] private ProjectilePool projectilePool;
    [SerializeField] private Transform spawnPos;
    public bool idle;
    
    private AudioManager audioManager;
    private AudioSource audioSource;



    private void Start()
    {
        player = GameObject.Find("Player").transform;
        enemyAnimator = transform.GetComponentInChildren<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        enemyDead = false;
        audioSource = this.GetComponent<AudioSource>();
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
                idle = true;
                enemyJustSeen = false;
            }
            if (playerInSightRange && !playerInAttackRange)
            {
                ChasePlayer();
                idle = false;
            }
            if (playerInSightRange && playerInAttackRange)
            {
                AttackPlayer();
                idle = false;
            }
        }
    }

    public override void ChasePlayer()
    {
        transform.LookAt(player);
        transform.Rotate(270f, 180, 270f);
        if (!enemyJustSeen)
        {
            enemyAnimator.SetTrigger("PlayerSeen");
            Invoke(nameof(EndScreamAnimation), screamTime);
        }
        else
        {
            enemyAnimator.SetBool("StartChasing", true);
            // Dirección hacia el jugador
            Vector3 direction = (player.position - this.transform.position).normalized;
            direction = AvoidObstacles(direction);
            // Movimiento suave
            transform.position += direction * moveSpeed * Time.deltaTime;
            MaintainHeightRelativeToPlayer();
        }
    }

    private Vector3 AvoidObstacles(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, obstacleAvoidanceRange, ~0, QueryTriggerInteraction.Ignore))
        {
            // Si hay pared adelante, desviarse hacia la derecha
            return Vector3.Cross(Vector3.up, hit.normal).normalized;
        }
        return direction;
    }

    private void MaintainHeightRelativeToPlayer()
    {
        float targetHeight = player.position.y + heightOffset;
        Vector3 pos = transform.position;
        pos.y = Mathf.Lerp(pos.y, targetHeight, heightAdjustSpeed * Time.deltaTime);
        transform.position = pos;
    }

    public override void EndScreamAnimation()
    {
        enemyJustSeen = true;
    }

    public override void AttackPlayer()
    {
        enemyAnimator.SetBool("StartChasing", false);
        transform.LookAt(player);
        transform.Rotate(270f, 180f, 270f);

        if (!alreadyAttacked)
        {
            enemyAnimator.SetTrigger("AttackPlayer");
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }

    private void PlayAudioClip(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    public void Shoot()
    {
        PlayAudioClip(audioManager.ufoShoot);
        Vector3 direction = (player.position - spawnPos.position).normalized;
        GameObject proj = projectilePool.GetProjectile(spawnPos.position, transform.rotation);
        proj.GetComponent<Projectile>().Init(projectilePool);
        proj.GetComponent<Rigidbody>().AddForce(direction * projectileForce, ForceMode.Impulse);
    }

    public override void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public override void EnemyDied()
    {
        playerInSightRange = false;
        playerInAttackRange = false;
        enemyDead = true;
    }

    public override void EnemyRevived(Vector3 originalPos)
    {
        transform.position = originalPos;
        playerInSightRange = false;
        playerInAttackRange = false;
        enemyDead = false;
    }
}
