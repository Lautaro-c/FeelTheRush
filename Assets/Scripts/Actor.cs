using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField] private Vector3 originalPosition;
    int currentHealth;
    public int maxHealth;
    public event Action OnDie;
    private Animator enemyAnimator;
    private Transform cameraTransform;
    private float force = 50f;
    private float upForce = 10f;
    private Rigidbody enemyRb;
    private CapsuleCollider enemyCc;
    // 0 = normal || 1 = Goomba
    private int killType = 0; 

    void Awake()
    {
        currentHealth = maxHealth;
        OnDie += PlayDeathAnimation;
        OnDie += SendEnemyFlying;
    }

    private void Start()
    {
        enemyAnimator = GetComponentInChildren<Animator>();
        cameraTransform = GameObject.Find("Main Camera").transform;
        enemyRb = this.GetComponent<Rigidbody>();
        enemyCc = this.GetComponent<CapsuleCollider>();
    }

    public void TakeDamage(int amount, int AttackType)
    {
        currentHealth -= amount;
        killType = AttackType;
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        ScoreManager.Instance?.RegisterKill();
        OnDie.Invoke();
        StartCoroutine(WaitToDestroy());
    }

    private IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(1f);
        DestroyEnemy();
    }

    private void PlayDeathAnimation()
    {
        enemyAnimator.SetTrigger("EnemyDied");
    }

    private void SendEnemyFlying()
    {
        switch (killType)
        {
            case 0:
                if (enemyRb != null && cameraTransform != null && enemyCc != null)
                {
                    enemyCc.isTrigger = true;
                    enemyRb.useGravity = true;
                    Vector3 shootDirection = cameraTransform.forward;
                    enemyRb.AddForce(shootDirection.normalized * force, ForceMode.Impulse);
                }
                break;
            case 1:
                if (enemyRb != null && enemyCc != null)
                {
                    enemyCc.isTrigger = true;
                    enemyRb.useGravity = true;
                    Vector3 shootDirection = -transform.up;
                    enemyRb.AddForce(shootDirection.normalized * force, ForceMode.Impulse);
                }
                break;
        }
    }

    public void SendEnemyFlyingUp()
    {
        if (enemyRb != null && enemyCc != null)
        {
            enemyRb.useGravity = true;
            Vector3 shootDirection = transform.up;
            enemyRb.AddForce(shootDirection.normalized * upForce, ForceMode.Impulse);
        }
    }

    private void DestroyEnemy()
    {
        this.gameObject.SetActive(false);
    }

    public void RestartEnemy()
    {
        currentHealth = maxHealth;
        if (enemyRb != null)
        {
            enemyRb.linearVelocity = Vector3.zero;
            enemyRb.angularVelocity = Vector3.zero;
            enemyRb.useGravity = false;
        }
        if (enemyCc != null)
        {
            enemyCc.isTrigger = false;
        }
        if (enemyAnimator != null)
        {
            enemyAnimator.ResetTrigger("EnemyDied");
        }
        transform.position = originalPosition;
        gameObject.SetActive(true);
    }
}